using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV1;
using System;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class ItemServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ItemServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Items.Add(new Item(
                    "P000001",
                    "Test Code",
                    "Test Item",
                    "Test Item",
                    "1234567890",
                    "Test Model",
                    "Test Commodity",
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    "Test Supplier",
                    "Test Part Number"
                ));
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneItem()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemService = new ItemsServiceV1(context);
                var Items = await ItemService.GetItems();

                Assert.NotNull(Items);
                Assert.Single(Items);
                Assert.Equal("P000001", Items[0].Uid);
                Assert.Equal("Test Code", Items[0].Code);
                Assert.Equal("Test Item", Items[0].Description);
                Assert.Equal("Test Item", Items[0].ShortDescription);
                Assert.Equal("1234567890", Items[0].UpcCode);
                Assert.Equal("Test Model", Items[0].ModelNumber);
                Assert.Equal("Test Commodity", Items[0].CommodityCode);
                Assert.Equal(1, Items[0].ItemLine);
                Assert.Equal(1, Items[0].ItemGroup);
                Assert.Equal(1, Items[0].ItemType);
                Assert.Equal(1, Items[0].UnitPurchaseQuantity);
                Assert.Equal(1, Items[0].UnitOrderQuantity);
                Assert.Equal(1, Items[0].PackOrderQuantity);
                Assert.Equal(1, Items[0].SupplierId);
                Assert.Equal("Test Supplier", Items[0].SupplierCode);
                Assert.Equal("Test Part Number", Items[0].SupplierPartNumber);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddItem()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemService = new ItemsServiceV1(context);
                await ItemService.AddItem(new Item(
                    "P000002",
                    "Test Code 2",
                    "Test Item 2",
                    "Test Item 2",
                    "1234567890",
                    "Test Model 2",
                    "Test Commodity 2",
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    "Test Supplier 2",
                    "Test Part Number 2"
                ));
            }

            using (var context = new CargoHubContext(options))
            {
                var Items = context.Items.ToList();

                Assert.Equal(2, Items.Count);
                Assert.Equal("P000002", Items[1].Uid);
                Assert.Equal("Test Code 2", Items[1].Code);
                Assert.Equal("Test Item 2", Items[1].Description);
                Assert.Equal("Test Item 2", Items[1].ShortDescription);
                Assert.Equal("1234567890", Items[1].UpcCode);
                Assert.Equal("Test Model 2", Items[1].ModelNumber);
                Assert.Equal("Test Commodity 2", Items[1].CommodityCode);
                Assert.Equal(2, Items[1].ItemLine);
                Assert.Equal(2, Items[1].ItemGroup);
                Assert.Equal(2, Items[1].ItemType);
                Assert.Equal(2, Items[1].UnitPurchaseQuantity);
                Assert.Equal(2, Items[1].UnitOrderQuantity);
                Assert.Equal(2, Items[1].PackOrderQuantity);
                Assert.Equal(2, Items[1].SupplierId);
                Assert.Equal("Test Supplier 2", Items[1].SupplierCode);
                Assert.Equal("Test Part Number 2", Items[1].SupplierPartNumber);
            }

        }

        [Fact, TestPriority(2)]
        public async void UpdateItem()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemService = new ItemsServiceV1(context);
                var Item = context.Items.First();
                Item.Description = "Updated Item";
                await ItemService.UpdateItem(Item.Uid, Item);
            }

            using (var context = new CargoHubContext(options))
            {
                var Items = context.Items.ToList();

                Assert.Single(Items);
                Assert.Equal("P000001", Items[0].Uid);
                Assert.Equal("Test Code", Items[0].Code);
                Assert.Equal("Updated Item", Items[0].Description);
                Assert.Equal("Test Item", Items[0].ShortDescription);
                Assert.Equal("1234567890", Items[0].UpcCode);
                Assert.Equal("Test Model", Items[0].ModelNumber);
                Assert.Equal("Test Commodity", Items[0].CommodityCode);
                Assert.Equal(1, Items[0].ItemLine);
                Assert.Equal(1, Items[0].ItemGroup);
                Assert.Equal(1, Items[0].ItemType);
                Assert.Equal(1, Items[0].UnitPurchaseQuantity);
                Assert.Equal(1, Items[0].UnitOrderQuantity);
                Assert.Equal(1, Items[0].PackOrderQuantity);
                Assert.Equal(1, Items[0].SupplierId);
                Assert.Equal("Test Supplier", Items[0].SupplierCode);
                Assert.Equal("Test Part Number", Items[0].SupplierPartNumber);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveItem()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemService = new ItemsServiceV1(context);
                var Item = context.Items.First();
                await ItemService.RemoveItem(Item.Uid);
            }

            using (var context = new CargoHubContext(options))
            {
                var Items = context.Items.ToList();

                Assert.Empty(Items);
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