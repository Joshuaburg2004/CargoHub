using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CargoHubAlt.Interfaces.InterfacesV1;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Services.ServicesV1;
using CargoHubAlt.Services.ServicesV2;

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
        .Filter.ByIncludingOnly(evt => evt.Properties["SourceContext"].ToString().Contains("ClientController"))
        .WriteTo.File("Logs/ClientController.log"))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Properties["SourceContext"].ToString().Contains("ShipmentController"))
        .WriteTo.File("Logs/ShipmentController.log"))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Properties["SourceContext"].ToString().Contains("ItemController"))
        .WriteTo.File("Logs/ItemController.log"))
    .CreateLogger();

        builder.Host.UseSerilog();

        // V1
        builder.Services.AddTransient<IClientServiceV1, ClientServiceV1>();
        builder.Services.AddTransient<IInventoryServiceV1, InventoryServiceV1>();
        builder.Services.AddTransient<IItemTypeServiceV1, ItemTypeServiceV1>();
        builder.Services.AddTransient<IItemGroupServiceV1, ItemGroupServiceV1>();
        builder.Services.AddTransient<IItemLineServiceV1, ItemLineServiceV1>();
        builder.Services.AddTransient<IItemsServiceV1, ItemsServiceV1>();
        builder.Services.AddTransient<IWarehouseServiceV1, WarehouseServiceV1>();
        builder.Services.AddTransient<IShipmentServiceV1, ShipmentServiceV1>();
        builder.Services.AddTransient<ITransferServiceV1, TransferServiceV1>();
        builder.Services.AddTransient<ILocationServiceV1, LocationServiceV1>();
        builder.Services.AddTransient<ISupplierServiceV1, SuppliersV1>();
        builder.Services.AddTransient<IOrderServiceV1, OrderServiceV1>();
        // V2
        builder.Services.AddTransient<IClientServiceV2, ClientServiceV2>();
        builder.Services.AddTransient<IInventoryServiceV2, InventoryServiceV2>();
        builder.Services.AddTransient<IItemTypeServiceV2, ItemTypeServiceV2>();
        builder.Services.AddTransient<IItemGroupServiceV2, ItemGroupServiceV2>();
        builder.Services.AddTransient<IItemLineServiceV2, ItemLineServiceV2>();
        builder.Services.AddTransient<IItemsServiceV2, ItemsServiceV2>();
        builder.Services.AddTransient<IWarehouseServiceV2, WarehouseServiceV2>();
        builder.Services.AddTransient<IShipmentServiceV2, ShipmentServiceV2>();
        builder.Services.AddTransient<ITransferServiceV2, TransferServiceV2>();
        builder.Services.AddTransient<ILocationServiceV2, LocationServiceV2>();
        builder.Services.AddTransient<ISupplierServiceV2, SuppliersV2>();
        builder.Services.AddTransient<IOrderServiceV2, OrderServiceV2>();
        builder.Services.AddTransient<IBackupService, BackupService>();

        builder.Services.AddScoped<ApiKeyActionFilter>();
        builder.Services.AddControllers();
        builder.Services.AddControllers(options =>
        {
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
