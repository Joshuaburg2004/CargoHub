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
    public DbSet<Location> Locations { get; set; }
    public DbSet<Item_group> Item_Groups { get; set; }
    public DbSet<Item_line> Item_Lines { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Item_type> Item_Types { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().OwnsMany(e => e.Items);
        modelBuilder.Entity<Order>().HasKey(e => e.Id);
        modelBuilder.Entity<Shipment>().OwnsMany(e => e.Items);
        modelBuilder.Entity<Shipment>().HasKey(e => e.Id);
        modelBuilder.Entity<Transfer>().OwnsMany(e => e.Items);
        modelBuilder.Entity<Transfer>().HasKey(e => e.Id);
    }
}