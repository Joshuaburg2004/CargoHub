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
    public class InventoryServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public InventoryServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Inventories.Add(new Inventory
                {
                    Id = 1,
                    ItemId = "Test Item",
                    Description = "Test Description",
                    ItemReference = "Test Reference",
                    Locations = new List<int> { 1 },
                    TotalOnHand = 10,
                    TotalExpected = 20,
                    TotalOrdered = 30,
                    TotalAllocated = 5,
                    TotalAvailable = 5

                });
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneInventory()
        {
            using (var context = new CargoHubContext(options))
            {
                var InventoryService = new InventoryServiceV1(context);
                var Inventories = await InventoryService.GetAllInventories();

                Assert.NotNull(Inventories);
                Assert.Single(Inventories);
                Assert.Equal("Test Item", Inventories[0].ItemId);
                Assert.Equal("Test Description", Inventories[0].Description);
                Assert.Equal("Test Reference", Inventories[0].ItemReference);
                Assert.Equal(1, Inventories[0].Locations[0]);
                Assert.Equal(10, Inventories[0].TotalOnHand);
                Assert.Equal(20, Inventories[0].TotalExpected);
                Assert.Equal(30, Inventories[0].TotalOrdered);
                Assert.Equal(5, Inventories[0].TotalAllocated);
                Assert.Equal(5, Inventories[0].TotalAvailable);

            }
        }

        [Fact, TestPriority(1)]
        public async void AddInventory()
        {
            using (var context = new CargoHubContext(options))
            {
                var InventoryService = new InventoryServiceV1(context);
                await InventoryService.CreateInventory(new Inventory
                {
                    Id = 2,
                    ItemId = "Test Item 2",
                    Description = "Test Description 2",
                    ItemReference = "Test Reference 2",
                    Locations = new List<int> { 2 },
                    TotalOnHand = 20,
                    TotalExpected = 30,
                    TotalOrdered = 40,
                    TotalAllocated = 10,
                    TotalAvailable = 10
                });
            }

            using (var context = new CargoHubContext(options))
            {
                var Inventorys = context.Inventories.ToList();

                Assert.Equal(2, Inventorys.Count);
                Assert.Equal("Test Item 2", Inventorys[1].ItemId);
                Assert.Equal("Test Description 2", Inventorys[1].Description);
                Assert.Equal("Test Reference 2", Inventorys[1].ItemReference);
                Assert.Equal(2, Inventorys[1].Locations[0]);
                Assert.Equal(20, Inventorys[1].TotalOnHand);
                Assert.Equal(30, Inventorys[1].TotalExpected);
                Assert.Equal(40, Inventorys[1].TotalOrdered);
                Assert.Equal(10, Inventorys[1].TotalAllocated);
                Assert.Equal(10, Inventorys[1].TotalAvailable);
            }

        }

        [Fact, TestPriority(2)]
        public async void UpdateInventory()
        {
            using (var context = new CargoHubContext(options))
            {
                var InventoryService = new InventoryServiceV1(context);
                var Inventory = context.Inventories.First();
                Inventory.ItemId = "Updated Inventory";
                await InventoryService.UpdateInventory(Inventory.Id, Inventory);
            }

            using (var context = new CargoHubContext(options))
            {
                var Inventorys = context.Inventories.ToList();

                Assert.Single(Inventorys);
                Assert.Equal("Updated Inventory", Inventorys[0].ItemId);
                Assert.Equal("Test Description", Inventorys[0].Description);
                Assert.Equal("Test Reference", Inventorys[0].ItemReference);
                Assert.Equal(1, Inventorys[0].Locations[0]);
                Assert.Equal(10, Inventorys[0].TotalOnHand);
                Assert.Equal(20, Inventorys[0].TotalExpected);
                Assert.Equal(30, Inventorys[0].TotalOrdered);
                Assert.Equal(5, Inventorys[0].TotalAllocated);
                Assert.Equal(5, Inventorys[0].TotalAvailable);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveInventory()
        {
            using (var context = new CargoHubContext(options))
            {
                var InventoryService = new InventoryServiceV1(context);
                var Inventory = context.Inventories.First();
                await InventoryService.DeleteInventory(Inventory.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Inventorys = context.Inventories.ToList();

                Assert.Empty(Inventorys);
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