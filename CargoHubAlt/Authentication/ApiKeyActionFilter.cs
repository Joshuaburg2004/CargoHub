using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Text.Json; 
public class ApiKeyActionFilter : Attribute, IAsyncActionFilter{
    public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next){
        var context = actionContext.HttpContext;
        var options = context.RequestServices.GetService<IOptions<ApiKeyOptions>>() switch {
            { Value: var __ } => __,
            _ => new ApiKeyOptions() { Users = new List<User>() } 
        };
        if(!context.Request.Headers.ContainsKey("api_key")){
            context.Response.StatusCode = 401;
            Console.WriteLine($"{context.Request.Path} was requested but there is no api_key");
            return;
        }
        User? usery = null;
        foreach(var header in context.Request.Headers){
            Console.WriteLine($"{header.Key}: {header.Value}");
        }
        foreach(var user in options.Users){
            if(context.Request.Headers["api_key"] != user.api_key){
                Console.WriteLine(user.api_key);
                continue;
            }
            usery = user;
        }
        if(usery == null){
            context.Response.StatusCode = 401;
            Console.WriteLine("api_key is invalid");
            return;
        }
        var path = context.Request.Path.ToString().Replace("/api/v1/", "").Split("/");
        Console.WriteLine(path);
        if(usery.endpoint_access[path[0]].ContainsKey(context.Request.Method)){
            if(usery.endpoint_access[context.Request.Path][context.Request.Method]){
                await next();
                context.Response.StatusCode = 200;
                return;
            }
            context.Response.StatusCode = 401;
            Console.WriteLine("User does not have access to this endpoint");
            return;
        }
        await next();
        context.Response.StatusCode = 200;
        return;
    }
}
