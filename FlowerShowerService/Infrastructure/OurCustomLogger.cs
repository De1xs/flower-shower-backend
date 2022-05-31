namespace FlowerShowerService.Infrastructure;
using FlowerShowerService.Data.Entities;
using Microsoft.AspNetCore.Mvc.Controllers;

public sealed class OurCustomLogger
{
    private readonly RequestDelegate _next;

    public OurCustomLogger(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILoggingService service)
    {
        if (context.Request.Headers.TryGetValue("UserId", out var userIdHeader))
        {
            var userId = int.Parse(userIdHeader);

            var endpoint = context.GetEndpoint()!.Metadata.GetMetadata<ControllerActionDescriptor>();

            var logEntry = new LogEntry
            {
                UserId = userId,
                Role = userId == 0 ? "Admin" : "User",
                Endpoint = endpoint.ControllerTypeInfo.FullName + '.' + endpoint.ActionName,
                Message = $"User: {userId} used {endpoint.ControllerTypeInfo.FullName} class and {endpoint.ActionName} method",
                Logged = DateTime.Now
            };

            await service.Log(logEntry);
        }

        await _next(context);
    }
}

public static class OurCustomLoggerMiddlewareExtensions
{
    public static IApplicationBuilder UseOurCustomLogger(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OurCustomLogger>();
    }
}