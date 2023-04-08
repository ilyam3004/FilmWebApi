using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WatchlistService.Common.Exceptions;

namespace WatchlistService.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(Exception ex)
    {
        return ex switch
        {
            ValidationException => ValidationProblem(ex),
            DuplicateWatchlistException => Problem(ex.Message, statusCode: StatusCodes.Status409Conflict),
            // InvalidCredentialsException => Problem(ex.Message,
            //     statusCode: StatusCodes.Status401Unauthorized),
            _ => StatusCode(500, "Internal Server Error")
        };
    }

    private IActionResult ValidationProblem(Exception ex)
    {
        var validationException = (ValidationException) ex;
        
        var modelStateDictionary = new ModelStateDictionary();
        
        foreach (var error in validationException.Errors)
        {
            modelStateDictionary.AddModelError(
                error.PropertyName,
                error.ErrorMessage);
        }

        return ValidationProblem(modelStateDictionary);
    }
}