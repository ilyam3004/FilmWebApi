using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserService.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exceptionResult = context.Exception;

        context.Result = new ObjectResult(new {error = "Internal Server error"})
        {
            StatusCode = 500
        };
        
        context.ExceptionHandled = true;
    }
}