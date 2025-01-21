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
    public class ItemLineServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ItemLineServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.ItemLines.Add(new ItemLine()
                {
                    Id = 1,
                    Name = "Test ItemLine",
                    Description = "Test Description",
                });
                context.Items.Add(new Item("P000001", "Test Code", "Test Description", "Test Short Description", "Test Upc Code", "Test Model Number", "Test Commodity Code", 1, 0, 0, 10, 10, 10, 10, "Test Supplier Code", "Test Supplier Part Number"));
                context.SaveChanges();
            }


            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneItemLine()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemLineService = new ItemLineServiceV2(context);
                List<ItemLine> ItemLines = (await ItemLineService.GetAllItemLine()).ToList();

                Assert.NotNull(ItemLines);
                Assert.Single(ItemLines);
                Assert.Equal("Test ItemLine", ItemLines[0].Name);
                Assert.Equal("Test Description", ItemLines[0].Description);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddItemLine()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemLineService = new ItemLineServiceV2(context);
                var ItemLine = new ItemLine
                {
                    Name = "Test ItemLine 2",
                    Description = "Test Description 2",
                };

                await ItemLineService.AddItemLine(ItemLine);
                var ItemLines = (await ItemLineService.GetAllItemLine()).ToList();

                Assert.NotNull(ItemLines);
                Assert.Equal(2, ItemLines.Count());
                Assert.Equal("Test ItemLine 2", ItemLines[1].Name);
                Assert.Equal("Test Description 2", ItemLines[1].Description);
            }
        }

        [Fact, TestPriority(2)]
        public async void GetItemsfromItemLineById()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemLineService = new ItemLineServiceV2(context);
                var ItemLines = (await ItemLineService.GetAllItemLine()).ToList();
                var Items = await ItemLineService.GetItemsfromItemLineById(ItemLines[0].Id);
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
                Assert.Equal(1, ItemsList[0].ItemLine);
            }
        }

        [Fact, TestPriority(3)]
        public async void UpdateItemLine()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemLineService = new ItemLineServiceV2(context);
                var ItemLine = context.ItemLines.First();
                ItemLine.Name = "Updated ItemLine";
                await ItemLineService.UpdateItemLine(ItemLine.Id, ItemLine);
            }

            using (var context = new CargoHubContext(options))
            {
                var ItemLines = context.ItemLines.ToList();

                Assert.Single(ItemLines);
                Assert.Equal("Updated ItemLine", ItemLines[0].Name);
                Assert.Equal("Test Description", ItemLines[0].Description);
            }
        }

        [Fact, TestPriority(4)]
        public async void RemoveItemLine()
        {
            using (var context = new CargoHubContext(options))
            {
                var ItemLineService = new ItemLineServiceV2(context);
                var ItemLine = context.ItemLines.First();
                await ItemLineService.DeleteItemLine(ItemLine.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var ItemLines = context.ItemLines.ToList();

                Assert.Empty(ItemLines);
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