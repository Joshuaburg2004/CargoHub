using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using CargoHubAlt.Database;
using System.Diagnostics.CodeAnalysis;
[ExcludeFromCodeCoverage]
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContextOptions<CargoHubContext> registration
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<CargoHubContext>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            // Add a new DbContextOptions<CargoHubContext> registration
            services.AddDbContext<CargoHubContext>(options =>
            {
                options.UseSqlite("DataSource=TestDatabase.db");
            });

            // Ensure the database is created and migrations are applied
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CargoHubContext>();

                // Reset database state
                db.Database.EnsureDeleted(); // Verwijdert de database
                db.Database.EnsureCreated(); // Maakt de database opnieuw aan
                db.SaveChanges();
            }
        });
    }
}