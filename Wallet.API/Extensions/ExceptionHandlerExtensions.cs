using Wallet.API.Exceptions;

namespace Wallet.API.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}