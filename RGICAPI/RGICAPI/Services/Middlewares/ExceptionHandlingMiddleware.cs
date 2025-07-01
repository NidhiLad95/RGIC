using CRUDOperations;
using RGIC.Infrastructure;
using RGICAPI.APIHelpers;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace RGICAPI.Services.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception error)
            {
                var response = context?.Response;

                if (response != null)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = error switch
                    {
                        NullReferenceException _ => (int)HttpStatusCode.InternalServerError,
                        KeyNotFoundException _ => (int)HttpStatusCode.NotFound,
                        UnauthorizedAccessException _ => (int)HttpStatusCode.Unauthorized,
                        InvalidOperationException _ => (int)HttpStatusCode.BadRequest,
                        RGICException _ => (int)HttpStatusCode.BadRequest,
                        _ => (int)HttpStatusCode.InternalServerError,
                    };

                    string resultMessage = Utilities.GetDataFromXML("Message.xml", "error", "internal-server-error-message") ?? "";

                    Response responseModel = new() { Status = false, Message = error.Message };
                    if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
                        responseModel.Message = $"{responseModel.Message} {resultMessage}";

                    var result = JsonSerializer.Serialize(responseModel);
                    string? controller = Convert.ToString(context?.Request.RouteValues["controller"], CultureInfo.CurrentCulture);
                    string? action = Convert.ToString(context?.Request?.RouteValues["action"], CultureInfo.CurrentCulture);

                    var _configuration = context?.RequestServices.GetService<IConfiguration>();

                    if (_configuration != null)
                    {
                        await Logger.ErrorAsync((controller ?? ""), (action ?? ""),
                            (Convert.ToString(context?.User?.Identity?.Name ?? "")), error, _configuration).ConfigureAwait(false);
                    }

                    if (result != null)
                        await response.WriteAsync(result).ConfigureAwait(false);
                }
            }
        }
    }
}
