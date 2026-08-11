using HRMS.Application;
using HRMS.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add Services

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// DB

// DI

// AutoMapper

// Versioning

// Build
var app = builder.Build();


// Middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
