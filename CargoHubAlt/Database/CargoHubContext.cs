using CargoHubAlt.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHubAlt.Database
{
    public class CargoHubContext : DbContext
    {
        public CargoHubContext(DbContextOptions<CargoHubContext> options) : base(options) { }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<ItemGroup> ItemGroups { get; set; }
        public virtual DbSet<ItemLine> ItemLines { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<PickingOrder> PickingOrders { get; set; }
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
            modelBuilder.Entity<Warehouse>().OwnsOne(w => w.Contact, a =>
            {
                a.Property<int>("WarehouseId");
                a.HasKey("WarehouseId");
            });
            modelBuilder.Entity<PickingOrder>().OwnsMany(p => p.Route, a =>
            {
                a.WithOwner().HasForeignKey("PickingOrderId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });
        }
    }
}