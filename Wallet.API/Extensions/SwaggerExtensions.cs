namespace Wallet.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }
    
    public static IApplicationBuilder UseSwaggerGenExt(this IApplicationBuilder app)
    {
        app.UseSwagger();
        return app;
    }
}