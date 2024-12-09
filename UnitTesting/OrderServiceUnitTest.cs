using Microsoft.EntityFrameworkCore;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV1;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
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

        [TestPriority(0), Fact]
        public async void GetOneOrder()
        {
            using var context = new CargoHubContext(options);
            var orderService = new OrderServiceV1(context);
            var orders = await orderService.GetOrders();

            Assert.NotNull(orders);
            Assert.Single(orders);
            Assert.Equal("Test Order", orders[0].Reference);
            Assert.Equal("Test Order Extra", orders[0].ReferenceExtra);
            Assert.Equal("Test Order Status", orders[0].OrderStatus);
            Assert.Equal("Test Order Notes", orders[0].Notes);
            Assert.Equal("Test Shipping Notes", orders[0].ShippingNotes);
            Assert.Equal("Test Picking Notes", orders[0].PickingNotes);
            Assert.Equal(1, orders[0].WarehouseId);
            Assert.Equal(0, orders[0].ShipTo);
            Assert.Equal(0, orders[0].BillTo);
            Assert.Equal(5, orders[0].ShipmentId);
            Assert.Equal(100.0, orders[0].TotalAmount);
            Assert.Equal(0.0, orders[0].TotalDiscount);
            Assert.Equal(0.0, orders[0].TotalTax);
            Assert.Equal(0.0, orders[0].TotalSurcharge);
            Assert.Single(orders[0].Items);
            Assert.Equal("P000001", orders[0].Items[0].ItemId);
            Assert.Equal(55, orders[0].Items[0].Amount);
        }

        [TestPriority(1), Fact]
        public void AddOrder()
        {
            using var context = new CargoHubContext(options);
            context.Orders.Add(new Order{
                    Id = 1,
                    SourceId = 5,
                    OrderDate = "2021-09-01",
                    RequestDate = "2021-09-01",
                    Reference = "New Test Order",
                    ReferenceExtra = "New Test Order Extra",
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
                }
            );
            
        }

        [TestPriority(2), Fact]
        public void UpdateOrder()
        {
            throw new System.NotImplementedException();
        }

        [TestPriority(3), Fact]
        public void RemoveOrder()
        {
            throw new System.NotImplementedException();
        }
    }
}