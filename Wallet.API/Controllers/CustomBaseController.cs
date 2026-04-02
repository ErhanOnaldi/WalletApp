using System.Net;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application;

namespace Wallet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    protected IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        if (result.StatusCode == HttpStatusCode.Created)
        {
            return Created(result.UrlAsCreated, result);
        }

        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return NoContent();
        }
        return new ObjectResult(result){StatusCode = result.StatusCode.GetHashCode()};
    }
    
    [NonAction]
    protected IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return NoContent();
        }
        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
    }
}