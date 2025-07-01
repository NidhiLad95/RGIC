using CRUDOperations;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace AuthLibrary
{
    public class DataAccess : IAuthLib
    {
        private readonly IConfiguration _configuration;
        private static string? _connectionString;

        public DataAccess(IConfiguration configuration, string connectionString)
        {
            _configuration = configuration;
            _connectionString = connectionString;
        }

        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public static void SetupDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return _configuration.GetSection(key);
        }

        public string AppSettingsKeys(string nodeName, string key)
        {
            return _configuration["" + nodeName + ":" + key + ""]!;
        }

        #region Sign up

        public async Task<T> SignUp<T>(string storedProcedureName, object spParamsPocoMapper)
        {
            T? response;

            using var connections = Connection;
            try
            {
                response = await connections.QueryFirstOrDefaultAsync<T>(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(spParamsPocoMapper),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    )!;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                    connections.Close();
            }
            return response!;
        }
        #endregion Sign up

        #region Login

        public async Task<Response<T>> ValidateLoginCredentials<T>(string spName, object spParametersClassObject, string password)
        {
            using var connections = Connection;
            try
            {

                DataSet ds = new DataSet();

                using (var result = await connections.QueryMultipleAsync(
                 sql: spName,
                 param: GenricsDynamicParamterMapper(spParametersClassObject),
                 commandTimeout: null,
                 commandType: CommandType.StoredProcedure))
                {

                    var user = await result.ReadFirstOrDefaultAsync<T>();
                    var response = await result.ReadFirstOrDefaultAsync<Response<T>>();
                    if (user != null && response != null)
                    {
                        response.Data = user;
                        if (!VerifyPasswordHash(password, (byte[])typeof(T).GetProperty("PasswordHash")!.GetValue(response!.Data, null)!, (byte[])typeof(T).GetProperty("PasswordSalt")!.GetValue(response.Data, null)!))
                        {
                            response.Status = false;
                            response.Message = "Invalid password";
                            // response.Data = default;
                            return response;
                        }
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "unf";
                    }


                    return response;
                }



                // return response;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                    connections.Close();
            }


        }



        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            var newPassword = BCryptNet.HashPassword(password, Encoding.ASCII.GetString(storedSalt));
            return (newPassword == Encoding.Default.GetString(storedHash));
        }

        public async Task<Response> Logout(string token, string spName, object spParametersPocoMapper)
        {
            Response response = new();
            using var connections = Connection;
            try
            {
                response = await connections.QueryFirstAsync<Response>(
                     sql: spName,
                     param: GenricsDynamicParamterMapper(spParametersPocoMapper),
                     commandTimeout: null,
                     commandType: CommandType.StoredProcedure);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State != ConnectionState.Closed)
                    connections.Close();
            }
        }

        public async Task<T> SavePassword<T>(string storedProcedureName, object spParamsPocoMapper)
        {
            T? response;

            using var connections = Connection;
            try
            {
                response = await connections.QueryFirstOrDefaultAsync<T>(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(spParamsPocoMapper),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    )!;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                    connections.Close();
            }
            return response!;
        }
        #endregion Login




        #region Common Methods



        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var mySalt = BCryptNet.GenerateSalt();
            passwordSalt = Encoding.ASCII.GetBytes(mySalt);
            passwordHash = Encoding.ASCII.GetBytes(BCryptNet.HashPassword(password, mySalt));
        }

        private static DynamicParameters GenricsDynamicParamterMapper(object tmodelObj)
        {
            var parameter = new DynamicParameters();

            Type tModelType = tmodelObj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                if (!property.CustomAttributes.Any(x => x.AttributeType.Name.Contains("Ignore")))
                {
                    if (property.PropertyType == typeof(DataTable))
                    {
                        parameter.Add(string.Concat("@", property.Name), ((DataTable)property.GetValue(tmodelObj)!).AsTableValuedParameter());
                    }
                    else if (property.Name == "OldPasswordHash" || property.Name == "OldPasswordSalt")
                    {
                        parameter.Add(string.Concat("@", property.Name), property.GetValue(tmodelObj), DbType.Binary);
                    }
                    else
                    {
                        parameter.Add(string.Concat("@", property.Name), property.GetValue(tmodelObj));
                    }
                }
            }

            return parameter;
        }

        #endregion Common Methods

        public Task<string> GenerateJWTToken(string userId, string email, string role, Dictionary<string, string> additionalClaims = null!, DateTime? expiration = null)
        {
            additionalClaims ??= [];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(_configuration["SecretKey"]!);

            var claims = new List<Claim>()
                {
                          new Claim(ClaimTypes.Name, userId),
                          new Claim(ClaimTypes.Role, role),
                          new Claim(ClaimTypes.Email, email),

                };


            foreach (var claim in additionalClaims)
            {
                claims.Add(new Claim(claim.Key, claim.Value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration ?? DateTime.UtcNow.AddMinutes(20),
                Audience = "",
                Issuer = "",
                IssuedAt = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Task.FromResult(tokenString);
        }

        public async Task<Response> SaveToken(string spName, object spParametersObject)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QuerySingleAsync<Response>(
                        sql: spName,
                        param: GenricsDynamicParamterMapper(spParametersObject),
                        commandTimeout: null,
                        commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                    connections.Close();
            }
        }

        #region Admin Authlab
        public async Task<Response<T>> ValidateAdminLoginCredentials<T>(string spName, object spParametersClassObject, string password)
        {
            using var connections = Connection;
            try
            {
                DataSet ds = new DataSet();
                
                using (var result = await connections.QueryMultipleAsync(
                 sql: spName,
                 param: GenricsDynamicParamterMapper(spParametersClassObject),
                 commandTimeout: null,
                 commandType: CommandType.StoredProcedure))
                {

                    var user = await result.ReadFirstOrDefaultAsync<T>();
                    //var statusMessage = await result.ReadFirstOrDefaultAsync<StatusMessage>();
                    var response = await result.ReadFirstOrDefaultAsync<Response<T>>();
                    if (user != null && response != null)
                    {
                        response.Data = user;
                        //response.Message = statusMessage.Message;
                        //response.Status = statusMessage.Status;

                        if (!VerifyPasswordHash(password, (byte[])typeof(T).GetProperty("PasswordHash")!.GetValue(response!.Data, null)!, (byte[])typeof(T).GetProperty("PasswordSalt")!.GetValue(response.Data, null)!))
                        {
                            response.Status = false;
                            response.Message = "Invalid password";
                            // response.Data = default;
                            return response;
                        }
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "auf";
                    }
                    return response;
                }
                //return response;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                    connections.Close();
            }
        }    
       

        #endregion
    }
}
