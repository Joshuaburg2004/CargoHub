using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV2;
using System;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class LocationServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public LocationServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Locations.Add(new Location
                {
                    Id = 1,
                    WarehouseId = 1,
                    Code = "Test Code",
                    Name = "Test Location"
                });
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneLocation()
        {
            using (var context = new CargoHubContext(options))
            {
                var LocationService = new LocationServiceV2(context);
                var Locations = await LocationService.GetAllLocations();

                Assert.NotNull(Locations);
                Assert.Single(Locations);
                Assert.Equal(1, Locations[0].WarehouseId);
                Assert.Equal("Test Code", Locations[0].Code);
                Assert.Equal("Test Location", Locations[0].Name);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddLocation()
        {
            using (var context = new CargoHubContext(options))
            {
                var LocationService = new LocationServiceV2(context);
                await LocationService.AddLocation(new Location
                {
                    Id = 2,
                    WarehouseId = 2,
                    Code = "Test Code 2",
                    Name = "Test Location 2"
                });
            }

            using (var context = new CargoHubContext(options))
            {
                var Locations = context.Locations.ToList();

                Assert.Equal(2, Locations.Count);
                Assert.Equal(2, Locations[1].WarehouseId);
                Assert.Equal("Test Code 2", Locations[1].Code);
                Assert.Equal("Test Location 2", Locations[1].Name);
            }

        }

        [Fact, TestPriority(2)]
        public async void UpdateLocation()
        {
            using (var context = new CargoHubContext(options))
            {
                var LocationService = new LocationServiceV2(context);
                var Location = context.Locations.First();
                Location.Name = "Updated Location";
                await LocationService.UpdateLocation(Location.Id, Location);
            }

            using (var context = new CargoHubContext(options))
            {
                var Locations = context.Locations.ToList();

                Assert.Single(Locations);
                Assert.Equal(1, Locations[0].WarehouseId);
                Assert.Equal("Test Code", Locations[0].Code);
                Assert.Equal("Updated Location", Locations[0].Name);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveLocation()
        {
            using (var context = new CargoHubContext(options))
            {
                var LocationService = new LocationServiceV2(context);
                var Location = context.Locations.First();
                await LocationService.DeleteLocation(Location.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Locations = context.Locations.ToList();

                Assert.Empty(Locations);
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