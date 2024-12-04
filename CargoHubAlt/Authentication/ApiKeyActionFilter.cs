using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Text.Json;
public class ApiKeyActionFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
    {
        var context = actionContext.HttpContext;
        var _users = ApiKeyOptions.Users;

        if (_users.Count == 0)
        {
            context.Response.StatusCode = 401;
            Console.WriteLine("No users found");
            return;
        }
        if (!context.Request.Headers.ContainsKey("api_key"))
        {
            context.Response.StatusCode = 401;
            Console.WriteLine($"{context.Request.Path} was requested but there is no api_key");
            return;
        }
        var user = _users.Find(x => x.ApiKey == context.Request.Headers["api_key"]);
        if (user == null)
        {
            context.Response.StatusCode = 401;
            Console.WriteLine("api_key is invalid");
            return;
        }
        var path = context.Request.Path.ToString()
            .Replace("/api/v1/", "")
            .Replace("/api/v2/", "")
            .Split("/");
        AccessLevel? access = path[0] switch
        {
            "clients" => user.EndpointAccess.Clients,
            "inventories" => user.EndpointAccess.Inventories,
            "items" => user.EndpointAccess.Items,
            "item_types" => user.EndpointAccess.ItemTypes,
            "item_groups" => user.EndpointAccess.ItemGroups,
            "item_lines" => user.EndpointAccess.ItemLines,
            "locations" => user.EndpointAccess.Locations,
            "orders" => user.EndpointAccess.Orders,
            "shipments" => user.EndpointAccess.Shipments,
            "suppliers" => user.EndpointAccess.Suppliers,
            "transfers" => user.EndpointAccess.Transfers,
            "warehouses" => user.EndpointAccess.Warehouses,
            _ => null
        };
        if (access == null)
        {
            context.Response.StatusCode = 404;
            Console.WriteLine("Endpoint not found");
            return;
        }
        int statusCode = CheckAccess(access, context.Request.Method);
        if (statusCode == 401)
        {
            context.Response.StatusCode = 401;
            Console.WriteLine("Access denied");
            return;
        }
        await next();
        context.Response.StatusCode = statusCode;
        return;
    }
    // returns StatusCode
    public int CheckAccess(AccessLevel access, string method)
    {
        if (access.Full)
        {
            return 200;
        }
        if (method == "GET" && access.Get)
        {
            return 200;
        }
        if (method == "POST" && access.Post)
        {
            return 200;
        }
        if (method == "PUT" && access.Put)
        {
            return 200;
        }
        if (method == "DELETE" && access.Delete)
        {
            return 200;
        }
        return 401;
    }
}
