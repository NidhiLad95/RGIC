using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGICAPI.Services.Filters;

namespace RGICAPI.Controllers
{
   // [ServiceFilter(typeof(ActivityLogFilterAttribute)), ServiceFilter(typeof(ValidationFilter))]
    public class BaseController : ControllerBase
    {
        protected string ControllerName => ControllerContext.RouteData?.Values["controller"]?.ToString() ?? "";
        protected string ActionName => ControllerContext.RouteData?.Values["action"]?.ToString() ?? "";
        protected string IpAddress => Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
        protected string? UserIdentity => User?.Identity?.Name;
        protected string? UserEmail => User?.Claims?.FirstOrDefault(x => x.Type == "email")?.Value;
        protected string? UserRole => User?.Claims?.FirstOrDefault(x => x.Type == "role")?.Value;
        protected Guid UserId => Guid.Parse(UserIdentity ?? Guid.Empty.ToString());
        protected string? ModuleName { get; private set; }

        protected void SetModuleName(string name)
        {
            ModuleName = name;
        }

        protected string GetActorName()
        {
            var nameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "userName");

            return nameClaim?.Value ?? "";
        }

        //protected void SetCookieAuth(string token)
        //{
        //    Response.Cookies.Append(ApplicationConstants.AuthTokenKey, token, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        SameSite = SameSiteMode.None,
        //        MaxAge = TimeSpan.FromDays(1)
        //    });
        //}

        protected void RemoveCookieAuth()
        {
            //Response.Cookies.Delete(ApplicationConstants.AuthTokenKey);
        }
    }
}
