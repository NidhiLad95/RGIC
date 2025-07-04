using Dapper;
using Microsoft.Data.SqlClient;
using RGIC.Infrastructure;
using System.Data;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RGICAPI.APIHelpers
{
    public static class Logger
    {


        public static async Task ErrorAsync(string controller, string action, string createdBy, Exception exception,
            [Optional] IConfiguration configuration,
            [Optional] string requestBody)
        {

            using IDbConnection db = new SqlConnection(RGICHelpers.GetAppSettingValue(configuration, "ConnectionStrings:connectionString"));

            await db.ExecuteScalarAsync("[UspErrorLogger]", new
            {
                controller,
                action,
                message = exception == null ? "Data Logging" : exception.Message + ((exception is SqlException sqlException) ? " error at " + sqlException.Procedure : ""),
                source = exception == null ? "" : ((exception is SqlException sqlException1) ? sqlException1.Procedure : exception.Source),
                stack_trace = (exception == null ? "" : exception.StackTrace),
                line_no = exception == null ? 0 : new StackTrace(exception, true)?.GetFrame(0)?.GetFileLineNumber(),
                createdBy,
                requestBody
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }

    }
}
