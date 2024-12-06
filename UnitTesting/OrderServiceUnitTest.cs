using Microsoft.EntityFrameworkCore;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV1;

namespace CargoHub.UnitTesting
{
    public class OrderServiceUnitTest
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public OrderServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Orders.Add(new Order
                {
                    Id = 1,
                    SourceId = 0,
                    OrderDate = "2021-09-01",
                    RequestDate = "2021-09-01",
                    Reference = "Test Order",
                    ReferenceExtra = "Test Order Extra",
                    OrderStatus = "Test Order Status",
                    Notes = "Test Order Notes",
                    ShippingNotes = "Test Shipping Notes",
                    PickingNotes = "Test Picking Notes",
                    WarehouseId = 1,
                    ShipTo = 0,
                    BillTo = 0,
                    ShipmentId = 5,
                    TotalAmount = 100.0,
                    TotalDiscount = 0.0,
                    TotalTax = 0.0,
                    TotalSurcharge = 0.0,
                    CreatedAt = "2021-09-01",
                    UpdatedAt = "2021-09-01",
                    Items = new List<OrderedItem>
                    {
                        new OrderedItem
                        {
                            ItemId = "P000001",
                            Amount = 55
                        }
                    }
                });
                context.SaveChanges();
            }


            this.options = options;
        }

        [Fact]
        public async void GetOneOrder()
        {
            using var context = new CargoHubContext(options);
            var orders = await context.Orders.ToListAsync();
            Assert.Single(orders);
            Assert.Equal("Test Order", orders[0].Reference);
        }

        [Fact]
        public void AddOrder()
        {
            using var context = new CargoHubContext(options);
            
        }

        [Fact]
        public void UpdateOrder()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void RemoveOrder()
        {
            throw new System.NotImplementedException();
        }
    }
}