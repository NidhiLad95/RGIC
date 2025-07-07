namespace RGICAPI.Services.Middlewares
{
    public class AccessKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AccessKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Only apply to API routes
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                var configuredKey = _configuration["AccessKey"];
                if (!context.Request.Headers.TryGetValue("X-Access-Key", out var extractedKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Access Key is missing.");
                    return;
                }

                if (!configuredKey.Equals(extractedKey))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Invalid Access Key.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
