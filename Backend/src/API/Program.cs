using System.Text.Json.Serialization;

using API.Infrastructure;
using API.Services;

using Application;
using Application.Features.Interfaces;

using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
.AddInfrastructure(builder.Configuration)
.AddApplication();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        context.ProblemDetails.Instance =
        $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions
        .Add("requestId", context.HttpContext.TraceIdentifier);
    };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition
        = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        policy
        .WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddScoped<IUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseExceptionHandler();

app.MapControllers();

app.UseCors("AllowAngular");

app.Run();
