using Azure;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRUDOperations
{
    public class CrudOperationDataAccess : ICrudOperationService
    {
        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;

        public CrudOperationDataAccess(IConfiguration configuration, string connectionString)
        {
            _configuration = configuration;
            _connectionString = connectionString;
        }

        public CrudOperationDataAccess(string connectionString)
        {
            _connectionString = connectionString!;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public string GetConnectionString(string connectionName)
        {
            return this._configuration!.GetConnectionString(connectionName)!;
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration!.GetSection(key);
        }

        public string AppSettingsKeys(string nodeName, string key)
        {
            return this._configuration!["" + nodeName + ":" + key + ""]!;
        }

        public async Task<T> GetSqlReadStream<T>(string spName, object parameters, Func<SqlMapper.GridReader, Task<T>> onRead)
        {
            using var connections = Connection;
            try
            {
                var query = await connections.QueryMultipleAsync(
                    sql: spName,
                    param: GenricsDynamicParamterMapper(parameters),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                return await onRead(query);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                {
                    connections.Close();
                }
            }
        }

        public Task<T> Insert<T>(string storedProcedureName, DynamicParameters parameters)
        {
            T response;
            using var connections = Connection;
            try
            {
                response = connections.QueryFirstOrDefault<T>(
                    sql: storedProcedureName,
                    param: parameters,
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
            return Task.FromResult(response);
        }

        public async Task<Response<T>> InsertAndGet<T>(string storedProcedureName, object parametersPocoObj)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(parametersPocoObj),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                Response<T> response = result.Read<Response<T>>().FirstOrDefault()!;
               // response.Data = response.Status ? result.Read<T>().FirstOrDefault() : default;
                return response;
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

        public Task<T> Update<T>(string storedProcedureName, DynamicParameters parameters)
        {
            T response;
            using var connections = Connection;
            try
            {
                response = connections.QueryFirstOrDefault<T>(
                    sql: storedProcedureName,
                    param: parameters,
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
            return Task.FromResult(response)!;
        }

        public Task<T> Delete<T>(string storedProcedureName, DynamicParameters parameters)
        {
            T response;
            using var connections = Connection;
            try
            {
                response = connections.QueryFirstOrDefault<T>(
                    sql: storedProcedureName,
                    param: parameters,
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
            return Task.FromResult(response);
        }

        //public Task<T> InsertUpdateDelete<T>(string storedProcedureName, object spParamsPocoMapper)
        //{
        //    T response;
        //    using var connections = Connection;
        //    try
        //    {
        //        response = connections.QueryFirstOrDefault<T>(
        //            sql: storedProcedureName,
        //            param: GenricsDynamicParamterMapper(spParamsPocoMapper),
        //            commandTimeout: null,
        //            commandType: CommandType.StoredProcedure
        //            )!;

        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (connections.State == ConnectionState.Open)
        //            connections.Close();
        //    }
        //    return Task.FromResult(response);
        //}



        
        public Task<Response<T>> InsertUpdateDelete<T>(string storedProcedureName, object spParamsPocoMapper)
        {
            Response<T> response; //= result.Read<Response<T>>().FirstOrDefault()!;
            //response.Data = response.Status ? result.Read<T>().FirstOrDefault() : default;
            //T response;
            using var connections = Connection;
            try
            {
               //var result = connections.QueryFirstOrDefault(
               //     sql: storedProcedureName,
               //     param: GenricsDynamicParamterMapper(spParamsPocoMapper),
               //     commandTimeout: null,
               //     commandType: CommandType.StoredProcedure
               //     )!;

                response = connections.QueryFirstOrDefault(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(spParamsPocoMapper),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    )!;

                //response = result.Read<Response<T>>().FirstOrDefault()!;
                //response.Data = response.Status ? result.Read<T>().FirstOrDefault() : default;
            }
            catch (Exception Ex)
            {
                throw;
            }
            finally
            {
                if (connections.State == ConnectionState.Open)
                    connections.Close();
            }
            return Task.FromResult(response);
        }


        public async Task<Response<T>> GetSingleRecord<T>(string storedProcedureName, DynamicParameters parameters)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                Response<T> response = result.Read<Response<T>>().FirstOrDefault()!;
                response.Data = response.Status ? result.Read<T>().FirstOrDefault() : default;
                return response;
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

        public async Task<Response<T>> GetSingleRecord<T>(string storedProcedureName, object spParamsPocoMapper)
        {
            // Response<T> response = new Response<T>();
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(spParamsPocoMapper),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                var user = await result.ReadFirstOrDefaultAsync<T>();
                var response = await result.ReadFirstOrDefaultAsync<Response<T>>();
                if (user != null && response != null)
                {
                    response.Data = user;
                    
                }
                else
                {
                    response.Status = false;
                    response.Message = "Authentication failed.";
                }

                
                return response;
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
            //return response;
        }

        public async Task<Response<T>> GetSingleRecord<T>(string storedProcedureName)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                Response<T> response = result.Read<Response<T>>().FirstOrDefault()!;
                response.Data = response.Status ? result.Read<T>().FirstOrDefault() : default;
                return response;
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

        public async Task<ResponseList<T>> GetPaginatedList<T>(string storedProcedureName, DynamicParameters parameters)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                ResponseList<T> response = result.Read<ResponseList<T>>().FirstOrDefault()!;
                response.Data = response.Status ? result.Read<T>().ToList() : default;
                response.TotalRecords = response.RecordsFiltered = response.Status ? result.Read<int>().SingleOrDefault() : 0;
                return response;
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

        public async Task<Response<Tuple<T1, List<T2>>>> GetSingleRecord<T1, T2>(string storedProcedureName, DynamicParameters parameters)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                Response<Tuple<T1, List<T2>>> response = result.Read<Response<Tuple<T1, List<T2>>>>().FirstOrDefault()!;
                var firstObject = response.Status ? result.Read<T1>().FirstOrDefault() : default;
                var listObject = response.Status ? result.Read<T2>()?.ToList() : default;
                response.Data = new Tuple<T1, List<T2>>(firstObject!, listObject!);

                return response;
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

        public async Task<Response<Tuple<T1, List<T2>>>> GetSingleRecord<T1, T2>(string storedProcedureName, object parameters)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(parameters),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                Response<Tuple<T1, List<T2>>> response = result.Read<Response<Tuple<T1, List<T2>>>>().FirstOrDefault()!;
                var firstObject = response.Status ? result.Read<T1>().FirstOrDefault() : default;
                var listObject = response.Status ? result.Read<T2>()?.ToList() : default;
                response.Data = new Tuple<T1, List<T2>>(firstObject!, listObject!);

                return response;
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

        public async Task<Response<Tuple<T1, List<T2>, List<T3>>>> GetRecord<T1, T2, T3>(string storedProcedureName, object parameters)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(parameters),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                

                var firstObject = await result.ReadFirstOrDefaultAsync<T1>()!;
                var listObject = result.Read<T2>().ToList()!;
                var secondList = result.Read<T3>()?.ToList()!;
                var response = await result.ReadFirstOrDefaultAsync<Response<Tuple<T1, List<T2>, List<T3>>>>();
                if (firstObject != null && listObject != null && secondList != null && response != null)
                {
                    response.Data = new Tuple<T1, List<T2>, List<T3>>(firstObject!, listObject!, secondList!);
                }
                return response!;

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

        public async Task<ResponseList<T>> GetList<T>(string storedProcedureName, DynamicParameters parameters)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                ResponseList<T> response = result.Read<ResponseList<T>>().FirstOrDefault()!;
                response.Data = response.Status ? result.Read<T>().ToList() : default;
                return response;
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

        public async Task<ResponseList<T>> GetList<T>(string storedProcedureName, object spParamsPocoMapper)
        {
            //ResponseList<T> response = new ResponseList<T>();
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(spParamsPocoMapper),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                var data = (await result.ReadAsync<T>()).ToList();

                var response = await result.ReadFirstOrDefaultAsync<ResponseList<T>>();
                

                if (data != null && response != null)
                {
                    response.Data = data;
                 
                }
                else
                {
                    response.Status = false;
                    response.Message = "Authentication failed.";
                }
                return response;
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


        public async Task<ResponseGetList<T>> GetList<T>(string storedProcedureName)
        {
            using var connections = Connection;
            ResponseGetList<T> response = new ResponseGetList<T>();
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );


                var data = (await result.ReadAsync<T>()).ToList();
                var statusMessage = await result.ReadFirstOrDefaultAsync<StatusMessage>();

                if (data != null && statusMessage != null)
                {
                    response.Data = data;
                    response.Message = statusMessage.Message;
                    response.Status = statusMessage.Status;
                    //response.Total_count = data.Count();
                    //response.TotalRecords = statusMessage.TotalRecords;

                }
                else
                {
                    response.Status = false;
                    response.Message = "Authentication failed.";
                }
                return response;

                
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



        public async Task<ResponseList<T>> GetPaginatedList<T>(string storedProcedureName, object spParamsPocoMapper)
        {
            using var connections = Connection;
            try
            {
                var result = await connections.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: GenricsDynamicParamterMapper(spParamsPocoMapper),
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                    );

                ResponseList<T> response = result.Read<ResponseList<T>>().FirstOrDefault()!;
                response.Data = response.Status ? result.Read<T>().ToList() : default!;
                response.TotalRecords = response.RecordsFiltered = response.Status ? result.Read<int>().SingleOrDefault() : 0;
                return response;
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

        //public Tuple<List<Model>,Response> GetPaginatedList<Model, Response>(string storedProcedureName, DynamicParameters parameters)
        //{
        //    using (var connections = Connection)
        //    {
        //        try
        //        {
        //            var result = connections.QueryMultipleAsync(
        //                sql: storedProcedureName,
        //                param: parameters,
        //                commandTimeout: null,
        //                commandType: CommandType.StoredProcedure
        //                ).Result;

        //            return Tuple.Create(result.Read<Model>().ToList(),
        //                                result.Read<Response>().FirstOrDefault());
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            if (connections.State == ConnectionState.Open)
        //                connections.Close();
        //        }

        //    }
        //}

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
                    else
                    {
                        parameter.Add(string.Concat("@", property.Name), property.GetValue(tmodelObj));
                    }
                }
            }

            return parameter;
        }
        public async Task<ResponseBindListMulti<T1, T2>> BindLists<T1, T2>(string storedProcedureName, object parameters)
        {
            using var connection = Connection;
            try
            {
                var result = await connection.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                );
                var firstObject = await result.ReadFirstOrDefaultAsync<T1>()!;
                var listObject = result.Read<T2>().ToList()!;
                var response = await result.ReadFirstOrDefaultAsync<ResponseBindListMulti<T1, T2>>();  // Expect List<List<T2>> here
                if (firstObject != null && listObject != null && response != null)
                {
                    response.List1 = firstObject!;
                    response.List2 = listObject!;  // Wrap the list
                }
                return response!;

                //var response = new ResponseBindListMulti<T1, T2>
                //{
                //    Status = true,
                //    Message = "Success"
                //};

                //// Read all result sets in order
                //response.List1 = result.Read<T1>().FirstOrDefault();
                //response.List2 = result.Read<T2>().ToList();


                //return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching dropdown lists.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public async Task<ResponseBindListMulti1<T1, T2>> BindOrderLists<T1, T2>(string storedProcedureName, object parameters)
        {
            using var connection = Connection;
            try
            {
                var result = await connection.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                );

                var response = new ResponseBindListMulti1<T1, T2>
                {
                    Status = true,
                    Message = "Success"
                };

                // Read all result sets in order
                response.List1 = result.Read<T1>().ToList();
                response.List2 = result.Read<T2>().ToList();


                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching dropdown lists.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public async Task<ResponseBindDropdownListMulti<T1, T2, T3, T4, T5, T6>> BindDropDownLists<T1, T2, T3, T4, T5, T6>(string storedProcedureName, object parameters)
        {
            using var connection = Connection;
            try
            {
                var result = await connection.QueryMultipleAsync(
                    sql: storedProcedureName,
                    param: parameters,
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure
                );

                var response = new ResponseBindDropdownListMulti<T1, T2, T3, T4, T5, T6>
                {
                    Status = true,
                    Message = "Success"
                };

                // Read all result sets in order
                response.List1 = result.Read<T1>().ToList();
                response.List2 = result.Read<T2>().ToList();
                response.List3 = result.Read<T3>().ToList();
                response.List4 = result.Read<T4>().ToList();
                response.List5 = result.Read<T5>().ToList();
                response.List6 = result.Read<T6>().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching dropdown lists.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        
        public Task<Response> BulkCopy(string destinationTableName, DataTable orderItems)
        {
            Response res = new Response();
            using var connections = Connection;

            // Cast the connection to SqlConnection for bulk copy
            if (connections is not SqlConnection sqlConnection)
            {
                throw new InvalidOperationException("The provided connection is not a valid SqlConnection for SqlBulkCopy.");
            }

            try
            {
                // Open the connection if it's not already open
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using var bulkCopy = new SqlBulkCopy(sqlConnection)
                {
                    DestinationTableName = destinationTableName // The target table name
                };

                // Map the DataTable columns to the database columns
                bulkCopy.ColumnMappings.Add("OrderID", "OrderID");
                bulkCopy.ColumnMappings.Add("BookID", "BookID");
                bulkCopy.ColumnMappings.Add("Discount", "Discount");
                bulkCopy.ColumnMappings.Add("Amount", "Amount");
                bulkCopy.ColumnMappings.Add("DiscountAmount", "DiscountAmount");
                bulkCopy.ColumnMappings.Add("Quantity", "Quantity");
                bulkCopy.ColumnMappings.Add("Price", "UnitPrice");//
                bulkCopy.ColumnMappings.Add("OrderStatus", "OrderStatus");

                // Perform the bulk copy
                bulkCopy.WriteToServer(orderItems);

                // Return success response
                res.Status = true;
                res.Message = "Data saved successfully";
                return Task.FromResult(res);
            }
            catch (Exception ex)
            {
                // Handle the exception
                res.Status = false;
                res.Message = $"An error occurred while inserting data: {ex.Message}";
                return Task.FromResult(res);
            }
            finally
            {
                // Ensure the connection is closed
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
