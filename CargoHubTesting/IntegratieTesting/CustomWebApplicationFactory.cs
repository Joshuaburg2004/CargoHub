using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing context
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<CargoHubContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add the in-memory database for testing
            services.AddDbContext<CargoHubContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            // Ensure the database is created
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CargoHubContext>();
                db.Database.EnsureCreated();
            }
        });
    }
}