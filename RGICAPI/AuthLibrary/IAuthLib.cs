using CRUDOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary
{
    public interface IAuthLib
    {

        Task<T> SignUp<T>(string storedProcedureName, object spParamsPocoMapper);
        Task<Response<T>> ValidateLoginCredentials<T>(string spName, object spParametersClassObject, string password);
        Task<string> GenerateJWTToken(string userId, string email, string role, Dictionary<string, string> additionalClaims = null!, DateTime? expiration = null);
        Task<Response> Logout(string authToken, string storedProcedureName, object spParametersObject);
        Task<T> SavePassword<T>(string storedProcedureName, object spParametersObject);

        Task<Response> SaveToken(string spName, object spParametersObject);

        #region Admin Authlab


        Task<Response<T>> ValidateAdminLoginCredentials<T>(string spName, object spParametersClassObject, string password);

        

        #endregion
    }
}

