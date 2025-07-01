using CRUDOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RGIC.Core.ActivityLogger.AcitvityLoggerDTOs;
using RGIC.Core.Common.CommonDTOs;
using System.Globalization;

namespace RGICAPI.Services.Filters
{
    public class ActivityLogFilterAttribute(ICrudOperationService crudOperationService) : ActionFilterAttribute
    {

        private readonly ICrudOperationService _crudOperationService = crudOperationService;
        private static string? Jsonrequest;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext?.ActionArguments;
            Jsonrequest = JsonConvert.SerializeObject(request);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                DTOActivityFilterResponse responseObj = new();
                var response = filterContext?.HttpContext.Response;
                var request = filterContext?.HttpContext.Request;
                if (filterContext?.Result is not FileContentResult)
                {
                    if (filterContext?.Result is ObjectResult Objresponse)
                    {
                        var Jsonresponse = JsonConvert.SerializeObject(Objresponse.Value);
                        responseObj = JsonConvert.DeserializeObject<DTOActivityFilterResponse>(Jsonresponse)!;
                    }
                }
                else
                {
                    var Jsonresponse = JsonConvert.SerializeObject(filterContext.Result);
                    responseObj = JsonConvert.DeserializeObject<DTOActivityFilterResponse>(Jsonresponse)!;
                    responseObj = new DTOActivityFilterResponse() { Message = responseObj?.FileDownloadName ?? "" + " file " };
                }

                string? ipAddress = null;

                // Try to get the IP address from the X-Forwarded-For header (in case of multiple proxies, it's comma-separated)
                var forwardedFor = filterContext?.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    var ips = forwardedFor.Split(',');
                    if (ips.Length > 0)
                    {
                        ipAddress = ips[0];
                    }
                }

                // If there is no X-Forwarded-For header, try the X-Real-IP header
                var realIp = filterContext?.HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
                if (!string.IsNullOrEmpty(realIp))
                {
                    ipAddress = realIp;
                }

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = filterContext?.HttpContext.Connection.RemoteIpAddress?.ToString();
                }

                // Generate the log of user activity
                LogUserActivity log = new()
                {
                    UserId = Guid.Parse(filterContext?.HttpContext?.User?.Identity?.Name ?? "F1673AE9-D195-4DAC-A516-46F56DE6791E"),
                    IpAddress = request?.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    AreaAccessed = request != null ? Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request) : "",
                    TimeStamp = DateTime.Now,
                    Body = Jsonrequest,
                    StatusCode = filterContext?.HttpContext.Response.StatusCode,
                    Method = request?.Method,
                    Description = responseObj?.Message != null ? Convert.ToString(responseObj?.Message, CultureInfo.CurrentCulture) : ""//responseObj.message.ToString()
                };
                //saves the log to database
                _crudOperationService.InsertUpdateDelete<Response>("[UspUserActivityloggerSave]", log).ConfigureAwait(false);

                if (filterContext != null)
                    base.OnResultExecuted(filterContext);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
