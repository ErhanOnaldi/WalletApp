using Microsoft.AspNetCore.Diagnostics;
using Wallet.Domain.Exceptions;

namespace Wallet.API.Exceptions;

public class CriticalExceptionHandler : IExceptionHandler
{
    //false döndürmek hatayı burda ele almayacağım demekken, true döndürmek hatayı HEMEN ele alacağım
    //anlamına gelir
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CriticalException)
        {
            Console.WriteLine($"Critical Exception: {exception.Message}");
        }
        
        return ValueTask.FromResult(false);
    }
}