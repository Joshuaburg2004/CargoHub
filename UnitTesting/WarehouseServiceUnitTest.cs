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
    public class WarehouseServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public WarehouseServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Warehouses.Add(new Warehouse
                {
                    Id = 1,
                    Code = "Test Code",
                    Name = "Test Warehouse",
                    Address = "123 Test St",
                    City = "Test City",
                    Zip = "12345",
                    Province = "Test Province",
                    Country = "Test Country",
                    Contact = new Contact("Test Contact", "123-456-7890", "testcontact@hr.nl")

                });
                context.Locations.Add(new Location
                {
                    Id = 1,
                    WarehouseId = 1,
                    Code = "Test Code",
                    Name = "Test Location",
                });
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneWarehouse()
        {
            using (var context = new CargoHubContext(options))
            {
                var WarehouseService = new WarehouseServiceV1(context);
                var Warehouses = await WarehouseService.GetAllWarehouses();

                Assert.NotNull(Warehouses);
                Assert.Single(Warehouses);
                Assert.NotNull(Warehouses[0].Contact);
                Assert.Equal("Test Warehouse", Warehouses[0].Name);
                Assert.Equal("Test Code", Warehouses[0].Code);
                Assert.Equal("123 Test St", Warehouses[0].Address);
                Assert.Equal("Test City", Warehouses[0].City);
                Assert.Equal("12345", Warehouses[0].Zip);
                Assert.Equal("Test Province", Warehouses[0].Province);
                Assert.Equal("Test Country", Warehouses[0].Country);
                Assert.Equal("Test Contact", Warehouses[0].Contact.Name);
                Assert.Equal("123-456-7890", Warehouses[0].Contact.Phone);
                Assert.Equal("testcontact@hr.nl", Warehouses[0].Contact.Email);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddWarehouse()
        {
            using (var context = new CargoHubContext(options))
            {
                var WarehouseService = new WarehouseServiceV1(context);
                await WarehouseService.AddWarehouse(new Warehouse
                {
                    Id = 2,
                    Code = "Test Code 2",
                    Name = "Test Warehouse 2",
                    Address = "123 Test St 2",
                    City = "Test City 2",
                    Zip = "12345",
                    Province = "Test Province 2",
                    Country = "Test Country 2",
                    Contact = new Contact("Test Contact 2", "123-456-7890", "testcontact2@hr.nl")
                });
            }

            using (var context = new CargoHubContext(options))
            {
                var Warehouses = context.Warehouses.ToList();

                Assert.Equal(2, Warehouses.Count);
                Assert.Equal("Test Warehouse 2", Warehouses[1].Name);
                Assert.Equal("123 Test St 2", Warehouses[1].Address);
                Assert.Equal("Test City 2", Warehouses[1].City);
                Assert.Equal("12345", Warehouses[1].Zip);
                Assert.Equal("Test Province 2", Warehouses[1].Province);
                Assert.Equal("Test Country 2", Warehouses[1].Country);
                Assert.Equal("Test Contact 2", Warehouses[1].Contact.Name);
                Assert.Equal("123-456-7890", Warehouses[1].Contact.Phone);
                Assert.Equal("testcontact2@hr.nl", Warehouses[1].Contact.Email);
            }

        }

        [Fact, TestPriority(2)]
        public async void UpdateWarehouse()
        {
            using (var context = new CargoHubContext(options))
            {
                var WarehouseService = new WarehouseServiceV1(context);
                var Warehouse = context.Warehouses.First();
                Warehouse.Name = "Updated Warehouse";
                await WarehouseService.UpdateWarehouse(Warehouse.Id, Warehouse);
            }

            using (var context = new CargoHubContext(options))
            {
                var Warehouses = context.Warehouses.ToList();

                Assert.Single(Warehouses);
                Assert.Equal("Updated Warehouse", Warehouses[0].Name);
                Assert.Equal("123 Test St", Warehouses[0].Address);
                Assert.Equal("Test City", Warehouses[0].City);
                Assert.Equal("12345", Warehouses[0].Zip);
                Assert.Equal("Test Province", Warehouses[0].Province);
                Assert.Equal("Test Country", Warehouses[0].Country);
                Assert.Equal("Test Contact", Warehouses[0].Contact.Name);
                Assert.Equal("123-456-7890", Warehouses[0].Contact.Phone);
                Assert.Equal("testcontact@hr.nl", Warehouses[0].Contact.Email);
            }
        }

        [Fact, TestPriority(3)]
        public async void GetLocationsfromWarehouseById()
        {
            using (var context = new CargoHubContext(options))
            {
                var WarehouseService = new WarehouseServiceV1(context);
                var Warehouse = context.Warehouses.First();
                var Locations = await WarehouseService.GetLocationsfromWarehouseById(Warehouse.Id);

                Assert.NotNull(Locations);
                Assert.Equal(1, Locations[0].WarehouseId);
                Assert.Equal("Test Location", Locations[0].Name);
                Assert.Equal("Test Code", Locations[0].Code);
            }
        }

        [Fact, TestPriority(4)]
        public async void RemoveWarehouse()
        {
            using (var context = new CargoHubContext(options))
            {
                var WarehouseService = new WarehouseServiceV1(context);
                var Warehouse = context.Warehouses.First();
                await WarehouseService.DeleteWarehouse(Warehouse.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Warehouses = context.Warehouses.ToList();

                Assert.Empty(Warehouses);
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