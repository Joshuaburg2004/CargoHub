using CargoHubAlt.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHubAlt.Database
{
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
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Item_line> ItemLines { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item_type> ItemTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().OwnsMany(o => o.Items, a =>
            {
                a.WithOwner().HasForeignKey("OrderId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

            modelBuilder.Entity<Shipment>().OwnsMany(s => s.Items, a =>
            {
                a.WithOwner().HasForeignKey("ShipmentId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

            modelBuilder.Entity<Transfer>().OwnsMany(t => t.Items, a =>
            {
                a.WithOwner().HasForeignKey("TransferId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });
        }
    }
}