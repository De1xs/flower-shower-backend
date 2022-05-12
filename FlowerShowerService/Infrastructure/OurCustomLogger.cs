using FlowerShowerService.Controllers;
using FlowerShowerService.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text;
using System.Text.Json;

namespace FlowerShowerService.Infrastructure;

public sealed class OurCustomLogger
{
    private readonly RequestDelegate _next;

    public OurCustomLogger(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var endpoint = context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();
        if(endpoint.ControllerTypeInfo.Name is nameof(UserController)) //this check won't be needed,
                                                                       //because we have to log every single request from FE as they are all business actions
        {
            //For now let's have it like this
            //Should create a task to implement database logging because of 7th requirment
            var request = context.Request;
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var requestContent = Encoding.UTF8.GetString(buffer);
            var userModel = JsonSerializer.Deserialize<UserModel>(requestContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            Console.WriteLine($"User: {userModel.Username} used {endpoint.ControllerTypeInfo.FullName} class and {endpoint.ActionName} method on {DateTime.Now}");

            request.Body.Position = 0;
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