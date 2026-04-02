using Wallet.API.Extensions;
using Wallet.Application.Extensions;
using Wallet.Persistence.Extensions;
using Wallet.Token.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGenExt();
builder.Services.AddPersistence(builder.Configuration).AddExceptionHandler().AddToken().AddApplications();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerGenExt();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();