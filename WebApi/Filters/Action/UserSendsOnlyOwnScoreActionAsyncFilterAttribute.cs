using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DataTransferObjects.Creating;

namespace WebApi.Filters.Action
{
    public class UserSendsOnlyOwnScoreActionAsyncFilterAttribute : TypeFilterAttribute
    {
        public UserSendsOnlyOwnScoreActionAsyncFilterAttribute() : base(typeof(UserSendsOnlyOwnScoreActionAsyncFilter))
        {
        }

        private class UserSendsOnlyOwnScoreActionAsyncFilter : IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "id")?.Value;
                if (context.ActionArguments["resultDto"] is ResultCreatingDto resultCreatingDto && userId != resultCreatingDto.UserId.ToString())
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                await next();
            }
        }
    }
}