using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Services;
using CargoHubAlt.Database;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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

        builder.Services.AddControllers();
        builder.Services.AddDbContext<CargoHubContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        app.Urls.Add("http://localhost:3000");
        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}
