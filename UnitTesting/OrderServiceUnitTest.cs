using Microsoft.EntityFrameworkCore;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV1;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class OrderServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        private readonly Order initOrder = new Order
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
        };
        private readonly Order orderToAdd = new Order
        {
            Id = 2,
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
        };
        private readonly Order orderToPut = new Order
        {
            Id = 1,
            SourceId = 5,
            OrderDate = "2021-09-01",
            RequestDate = "2021-09-01",
            Reference = "Updated Test Order",
            ReferenceExtra = "Updated Test Order Extra",
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
        };
        public OrderServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Orders.Add(initOrder);
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
            Assert.Equal(initOrder.Reference, orders[0].Reference);
            Assert.Equal(initOrder.ReferenceExtra, orders[0].ReferenceExtra);
            Assert.Equal(initOrder.OrderStatus, orders[0].OrderStatus);
            Assert.Equal(initOrder.Notes, orders[0].Notes);
            Assert.Equal(initOrder.ShippingNotes, orders[0].ShippingNotes);
            Assert.Equal(initOrder.PickingNotes, orders[0].PickingNotes);
            Assert.Equal(initOrder.WarehouseId, orders[0].WarehouseId);
            Assert.Equal(initOrder.ShipTo, orders[0].ShipTo);
            Assert.Equal(initOrder.BillTo, orders[0].BillTo);
            Assert.Equal(initOrder.ShipmentId, orders[0].ShipmentId);
            Assert.Equal(initOrder.TotalAmount, orders[0].TotalAmount);
            Assert.Equal(initOrder.TotalDiscount, orders[0].TotalDiscount);
            Assert.Equal(initOrder.TotalTax, orders[0].TotalTax);
            Assert.Equal(initOrder.TotalSurcharge, orders[0].TotalSurcharge);
            Assert.Single(orders[0].Items);
            Assert.Equal(initOrder.Items[0].ItemId, orders[0].Items[0].ItemId);
            Assert.Equal(initOrder.Items[0].Amount, orders[0].Items[0].Amount);
        }

        [TestPriority(1), Fact]
        public async void AddOrder()
        {
            using var context = new CargoHubContext(options);
            var orderService = new OrderServiceV1(context);
            var result = await orderService.AddOrder(orderToAdd);
            Assert.True(result);
            var orders = context.Orders.ToList();
            Assert.Equal(2, orders.Count);
            Assert.Equal(orderToAdd.Reference, orders[1].Reference);
            Assert.Equal(orderToAdd.ReferenceExtra, orders[1].ReferenceExtra);
            Assert.Equal(orderToAdd.OrderStatus, orders[1].OrderStatus);
            Assert.Equal(orderToAdd.Notes, orders[1].Notes);
            Assert.Equal(orderToAdd.ShippingNotes, orders[1].ShippingNotes);
            Assert.Equal(orderToAdd.PickingNotes, orders[1].PickingNotes);
            Assert.Equal(orderToAdd.WarehouseId, orders[1].WarehouseId);
            Assert.Equal(orderToAdd.ShipTo, orders[1].ShipTo);
            Assert.Equal(orderToAdd.BillTo, orders[1].BillTo);
            Assert.Equal(orderToAdd.ShipmentId, orders[1].ShipmentId);
            Assert.Equal(orderToAdd.TotalAmount, orders[1].TotalAmount);
            Assert.Equal(orderToAdd.TotalDiscount, orders[1].TotalDiscount);
            Assert.Equal(orderToAdd.TotalTax, orders[1].TotalTax);
            Assert.Equal(orderToAdd.TotalSurcharge, orders[1].TotalSurcharge);
            Assert.Single(orders[1].Items);
            Assert.Equal(orderToAdd.Items[0].ItemId, orders[1].Items[0].ItemId);
            Assert.Equal(orderToAdd.Items[0].Amount, orders[1].Items[0].Amount);
        }

        [TestPriority(2), Fact]
        public async void GetOrderedItems()
        {
            using var context = new CargoHubContext(options);
            var orderService = new OrderServiceV1(context);
            var orderedItems = await orderService.GetOrderedItems(1);
            Assert.NotNull(orderedItems);
            Assert.Single(orderedItems);
            Assert.Equal(initOrder.Items[0].ItemId, orderedItems[0].ItemId);
            Assert.Equal(initOrder.Items[0].Amount, orderedItems[0].Amount);
        }

        [TestPriority(3), Fact]
        public async void UpdateOrder()
        {
            using var context = new CargoHubContext(options);
            var orderService = new OrderServiceV1(context);
            var result = await orderService.UpdateOrder(orderToPut);
            Assert.True(result);
            var orders = context.Orders.ToList();
            Assert.Single(orders);
            Assert.Equal(orderToPut.Reference, orders[0].Reference);
            Assert.Equal(orderToPut.ReferenceExtra, orders[0].ReferenceExtra);
        }

        [TestPriority(4), Fact]
        public async void UpdateOrderedItems()
        {
            using var context = new CargoHubContext(options);
            var orderService = new OrderServiceV1(context);
            var orderedItems = new List<OrderedItem>
            {
                new OrderedItem
                {
                    ItemId = "P000002",
                    Amount = 10
                }
            };
            var result = await orderService.UpdateOrderedItems(1, orderedItems);
            Assert.True(result);
            var orders = context.Orders.ToList();
            var items = orders[0].Items.ToList();
            Assert.Single(items);
            Assert.Equal(orderedItems[0].ItemId, items[0].ItemId);
            Assert.Equal(orderedItems[0].Amount, items[0].Amount);
        }

        [TestPriority(5), Fact]
        public async void RemoveOrder()
        {
            using (var context = new CargoHubContext(options))
            {
                var orderService = new OrderServiceV1(context);
                var order = context.Orders.First();
                await orderService.RemoveOrder(order.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var orders = context.Orders.ToList();

                Assert.Empty(orders);
            }
        }
        public void Dispose()
        {
            using (var context = new CargoHubContext(options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}