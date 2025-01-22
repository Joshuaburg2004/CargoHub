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
    public class ItemTypeServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ItemTypeServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.ItemTypes.Add(new ItemType()
                {
                    Id = 1,
                    Name = "Test ItemType",
                    Description = "Test Description",
                });
                context.Items.Add(new Item("P000001", "Test Code", "Test Description", "Test Short Description", "Test Upc Code", "Test Model Number", "Test Commodity Code", 0, 0, 1, 10, 10, 10, 10, "Test Supplier Code", "Test Supplier Part Number"));
                context.SaveChanges();
            }


            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneItemType()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemTypeService = new ItemTypeServiceV2(context);
                List<ItemType> ItemTypes = (await ItemTypeService.GetAllItemType()).ToList();

                Assert.NotNull(ItemTypes);
                Assert.Single(ItemTypes);
                Assert.Equal("Test ItemType", ItemTypes[0].Name);
                Assert.Equal("Test Description", ItemTypes[0].Description);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddItemType()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemTypeService = new ItemTypeServiceV2(context);
                var ItemType = new ItemType
                {
                    Name = "Test ItemType 2",
                    Description = "Test Description 2",
                };

                await ItemTypeService.AddItemType(ItemType);
                var ItemTypes = (await ItemTypeService.GetAllItemType()).ToList();

                Assert.NotNull(ItemTypes);
                Assert.Equal(2, ItemTypes.Count());
                Assert.Equal("Test ItemType 2", ItemTypes[1].Name);
                Assert.Equal("Test Description 2", ItemTypes[1].Description);
            }
        }

        [Fact, TestPriority(2)]
        public async void GetItemsfromItemTypeById()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemTypeService = new ItemTypeServiceV2(context);
                var ItemTypes = (await ItemTypeService.GetAllItemType()).ToList();
                var Items = await ItemTypeService.GetItemsfromItemTypeById(ItemTypes[0].Id);
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
                Assert.Equal(1, ItemsList[0].ItemType);
            }
        }

        [Fact, TestPriority(3)]
        public async void UpdateItemType()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemTypeService = new ItemTypeServiceV2(context);
                var ItemType = context.ItemTypes.First();
                ItemType.Name = "Updated ItemType";
                await ItemTypeService.UpdateItemType(ItemType.Id, ItemType);
            }

            using (var context = new CargoHubContext(options))
            {
                var ItemTypes = context.ItemTypes.ToList();

                Assert.Single(ItemTypes);
                Assert.Equal("Updated ItemType", ItemTypes[0].Name);
                Assert.Equal("Test Description", ItemTypes[0].Description);
            }
        }

        [Fact, TestPriority(4)]
        public async void RemoveItemType()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemTypeService = new ItemTypeServiceV2(context);
                var ItemType = context.ItemTypes.First();
                await ItemTypeService.DeleteItemType(ItemType.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var ItemTypes = context.ItemTypes.ToList();

                Assert.Empty(ItemTypes);
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