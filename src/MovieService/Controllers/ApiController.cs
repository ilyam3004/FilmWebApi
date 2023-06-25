using Microsoft.AspNetCore.Mvc;
using MovieService.Common.Exceptions;

namespace MovieService.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(Exception ex)
    {
        return ex switch
        {
            WatchlistsNotFoundException => Problem(ex.Message,
                statusCode: StatusCodes.Status404NotFound),
            MovieNotFoundException => Problem(ex.Message, 
                statusCode: StatusCodes.Status404NotFound),
            _ => StatusCode(500, "Internal Server Error")
        };
    }
}