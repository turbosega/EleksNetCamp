using System.Threading.Tasks;
using BusinessLogicLayer.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters.Exception
{
    public class AsyncExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception != null)
            {
                switch (exception)
                {
                    case LoginIsTakenException e:
                        context.Result = new ConflictObjectResult(e.Message);
                        break;
                    case BadCredentialsException e:
                        context.Result = new UnprocessableEntityObjectResult(e.Message);
                        break;
                    case ResourceNotFoundException e:
                        context.Result = new NotFoundObjectResult(e.Message);
                        break;
                    case ValidationException e:
                        context.Result = new BadRequestObjectResult(e.Message);
                        break;
                    case NameOfResourceIsTakenException e:
                        context.Result = new ConflictObjectResult(e.Message);
                        break;
                    default:
                        context.HttpContext.Response.StatusCode = 500;
                        break;
                }

                context.ExceptionHandled = true;
            }
        }
    }
}