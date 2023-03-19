using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserService.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace UserService.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(Exception ex)
    {
        return ex switch
        {
            ValidationException => ValidationProblem(ex),
            DuplicateEmailException => Problem(ex.Message, 
                statusCode: StatusCodes.Status409Conflict),
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