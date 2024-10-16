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
        builder.Services.AddTransient<IItemGroupService, ItemGroupService>();


        builder.Services.AddControllers();
        builder.Services.AddDbContext<CargoHubContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Urls.Add("https://localhost:5000");
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}