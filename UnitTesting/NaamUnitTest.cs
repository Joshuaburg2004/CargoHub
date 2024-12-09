using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services;
using CargoHubAlt.Database;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class NaamServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public NaamServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Clients.Add(new Client
                {
                    Id = 1,
                    Name = "Test Client",
                    Address = "123 Test St",
                    City = "Test City",
                    ZipCode = "12345",
                    Province = "Test Province",
                    Country = "Test Country",
                    ContactName = "Test Contact",
                    ContactPhone = "123-456-7890",
                    ContactEmail = "test@example.com"
                });
                context.SaveChanges();
            }


            this.options = options;
        }

        [Fact, TestPriority(0)]
        public void GetOneNaam()
        {
            //voorbeeld
            // using (var context = new CargoHubContext(options))
            // {
            //     var WarehouseService = new WarehouseServiceV1(context);
            //     var Warehouses = await WarehouseService.GetAllWarehouses();

            //     Assert.NotNull(Warehouses);
            //     Assert.Single(Warehouses);
            //     Assert.Equal("Test Warehouse", Warehouses[0].Name);
            //     Assert.Equal("Test Code", Warehouses[0].Code);
            //     Assert.Equal("123 Test St", Warehouses[0].Address);
            //     Assert.Equal("Test City", Warehouses[0].City);
            //     Assert.Equal("12345", Warehouses[0].Zip);
            //     Assert.Equal("Test Province", Warehouses[0].Province);
            //     Assert.Equal("Test Country", Warehouses[0].Country);
            //     Assert.Equal("Test Contact", Warehouses[0].Contact.Name);
            //     Assert.Equal("123-456-7890", Warehouses[0].Contact.Phone);
            //     Assert.Equal("testcontact@hr.nl", Warehouses[0].Contact.Email);
            // }
        }

        [Fact, TestPriority(1)]
        public void AddNaam()
        {

        }

        [Fact, TestPriority(2)]
        public void UpdateNaam()
        {

        }

        [Fact, TestPriority(3)]
        public void RemoveNaam()
        {

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