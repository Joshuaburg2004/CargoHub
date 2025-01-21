using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV2;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class ItemGroupServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ItemGroupServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.ItemGroups.Add(new ItemGroup()
                {
                    Id = 1,
                    Name = "Test ItemGroup",
                    Description = "Test Description",
                });
                context.Items.Add(new Item("P000001", "Test Code", "Test Description", "Test Short Description", "Test Upc Code", "Test Model Number", "Test Commodity Code", 0, 1, 0, 10, 10, 10, 10, "Test Supplier Code", "Test Supplier Part Number"));
                context.SaveChanges();
            }


            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneItemGroup()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemGroupService = new ItemGroupServiceV2(context);
                List<ItemGroup> ItemGroups = (await ItemGroupService.GetAllItemGroup()).ToList();

                Assert.NotNull(ItemGroups);
                Assert.Single(ItemGroups);
                Assert.Equal("Test ItemGroup", ItemGroups[0].Name);
                Assert.Equal("Test Description", ItemGroups[0].Description);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddItemGroup()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemGroupService = new ItemGroupServiceV2(context);
                var ItemGroup = new ItemGroup
                {
                    Name = "Test ItemGroup 2",
                    Description = "Test Description 2",
                };

                await ItemGroupService.AddItemGroup(ItemGroup);
                var ItemGroups = (await ItemGroupService.GetAllItemGroup()).ToList();

                Assert.NotNull(ItemGroups);
                Assert.Equal(2, ItemGroups.Count());
                Assert.Equal("Test ItemGroup 2", ItemGroups[1].Name);
                Assert.Equal("Test Description 2", ItemGroups[1].Description);
            }
        }

        [Fact, TestPriority(2)]
        public async void GetItemsfromItemGroupById()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemGroupService = new ItemGroupServiceV2(context);
                var ItemGroups = (await ItemGroupService.GetAllItemGroup()).ToList();
                var Items = await ItemGroupService.GetItemsfromItemGroupById(ItemGroups[0].Id);
                Assert.NotNull(Items);
                var ItemsList = Items.ToList();

                Assert.NotNull(Items);
                Assert.Single(Items);
                Assert.Equal("P000001", ItemsList[0].Uid);
                Assert.Equal("Test Description", ItemsList[0].Description);
                Assert.Equal("Test Short Description", ItemsList[0].ShortDescription);
                Assert.Equal("Test Upc Code", ItemsList[0].UpcCode);
                Assert.Equal("Test Model Number", ItemsList[0].ModelNumber);
                Assert.Equal("Test Commodity Code", ItemsList[0].CommodityCode);
                Assert.Equal("Test Supplier Code", ItemsList[0].SupplierCode);
                Assert.Equal("Test Supplier Part Number", ItemsList[0].SupplierPartNumber);
                Assert.Equal(1, ItemsList[0].ItemGroup);
            }
        }

        [Fact, TestPriority(3)]
        public async void UpdateItemGroup()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemGroupService = new ItemGroupServiceV2(context);
                var ItemGroup = context.ItemGroups.First();
                ItemGroup.Name = "Updated ItemGroup";
                await ItemGroupService.UpdateItemGroup(ItemGroup.Id, ItemGroup);
            }

            using (var context = new CargoHubContext(options))
            {
                var ItemGroups = context.ItemGroups.ToList();

                Assert.Single(ItemGroups);
                Assert.Equal("Updated ItemGroup", ItemGroups[0].Name);
                Assert.Equal("Test Description", ItemGroups[0].Description);
            }
        }

        [Fact, TestPriority(4)]
        public async void RemoveItemGroup()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemGroupService = new ItemGroupServiceV2(context);
                var ItemGroup = context.ItemGroups.First();
                await ItemGroupService.DeleteItemGroup(ItemGroup.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var ItemGroups = context.ItemGroups.ToList();

                Assert.Empty(ItemGroups);
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