using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Infrastructure
{
    public static class RGICHelpers
    {
        public static string GetAppSettingValue(IConfiguration configuration, string key)
        {
            return "string";//configuration.GetValue<string>(key) ?? "";
        }

        public static int GetStatusCodeBykey(string key)
        {
            int statusCode;
            switch (key)
            {
                case "token-not-match":
                    {
                        statusCode = StatusCodes.Status410Gone;
                    }
                    break;
                case "security-response-not-matched":
                    {
                        statusCode = StatusCodes.Status410Gone;
                    }
                    break;
                case "duplicate":
                    {
                        statusCode = StatusCodes.Status409Conflict;
                    }
                    break;
                case "not-found":
                    {
                        statusCode = StatusCodes.Status404NotFound;
                    }
                    break;
                default:
                    {
                        statusCode = StatusCodes.Status500InternalServerError;
                    }
                    break;
                    ;

            }
            return statusCode;
        }

    }
}
