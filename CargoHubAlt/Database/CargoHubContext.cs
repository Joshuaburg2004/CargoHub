using Microsoft.EntityFrameworkCore;

public class CargoHubContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
}