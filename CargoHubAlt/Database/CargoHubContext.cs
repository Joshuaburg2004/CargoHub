using CargoHubAlt.Models;
using Microsoft.EntityFrameworkCore;

public class CargoHubContext : DbContext
{
    public CargoHubContext(DbContextOptions<CargoHubContext> options) : base(options) { }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Location> Locations {get; set;}
    public DbSet<Item_group> Item_Groups {get; set;}
    public DbSet<Item_line> Item_Lines {get; set;}
}