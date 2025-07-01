using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RGICAPI.Services.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var mesage = string.Join(Environment.NewLine, context.ModelState.Values
                                                       .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

                context.Result = new BadRequestObjectResult(new { status = false, message = mesage });
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
