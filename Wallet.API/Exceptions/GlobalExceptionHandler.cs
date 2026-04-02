using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Wallet.Application;

namespace Wallet.API.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorAsDto = ServiceResult.Fail(exception.Message,HttpStatusCode.InternalServerError);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);
        return true;
        
    }
}