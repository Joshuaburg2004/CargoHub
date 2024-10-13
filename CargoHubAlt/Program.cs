using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IWarehouseService, WarehouseService>();

        builder.Services.AddTransient<IShipmentService, ShipmentService>();


        builder.Services.AddControllers();

        var app = builder.Build();

        app.MapControllers();

        app.Urls.Add("https://localhost:5000");
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}







