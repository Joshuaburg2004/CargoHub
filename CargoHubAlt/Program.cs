using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Services;
using CargoHubAlt.Database;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
      .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Properties["SourceContext"].ToString().Contains("OrderController"))
        .WriteTo.File("Logs/OrderController.log"))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Properties["SourceContext"].ToString().Contains("TransferController"))
        .WriteTo.File("Logs/TransferController.log"))
    .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddTransient<IClientService, ClientService>();
        builder.Services.AddTransient<IInventoryService, InventoryService>();
        builder.Services.AddTransient<IItemTypeService, ItemTypeService>();
        builder.Services.AddTransient<IItemGroupService, ItemGroupService>();
        builder.Services.AddTransient<IItemLineService, ItemLineService>();
        builder.Services.AddTransient<IItemsService, ItemsService>();
        builder.Services.AddTransient<IWarehouseService, WarehouseService>();
        builder.Services.AddTransient<IShipmentService, ShipmentService>();
        builder.Services.AddTransient<ITransferService, TransferService>();
        builder.Services.AddTransient<ILocationService, LocationService>();
        builder.Services.AddTransient<ISupplierService, Suppliers>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddScoped<ApiKeyActionFilter>();
        builder.Services.AddControllers();
        builder.Services.AddControllers(options => {
            options.Filters.AddService<ApiKeyActionFilter>();
        });
        builder.Services.AddDbContext<CargoHubContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();
        

        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            await next.Invoke();
            Console.WriteLine($"{context.Connection.RemoteIpAddress} - - [{DateTime.Now}] \"{context.Request.Method} {context.Request.Path}\" {context.Response.StatusCode} -");
        });
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        app.Urls.Add("http://localhost:3000");
        app.MapGet("/", () => "Hello World!");
        Console.WriteLine("Serving on port 3000");
        app.Run();
    }
}
