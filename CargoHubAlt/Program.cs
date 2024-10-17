using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IClients, Clients>();
        builder.Services.AddTransient<IWarehouse, WarehouseService>();
        builder.Services.AddTransient<IShipmentService, ShipmentService>();
        builder.Services.AddTransient<ITransfer, TransferService>();
        builder.Services.AddTransient<ILocationService, LocationService>();
        builder.Services.AddTransient<IItemGroupService, ItemGroupService>();
        builder.Services.AddTransient<ISuppliers, Suppliers>();

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

        app.Urls.Add("https://localhost:5000");
        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}
