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
    public class NaamServiceUnitTest
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

        [Fact]
        public void GetOneNaam()
        {
            //voorbeeld
            // using (var context = new CargoHubContext(options))
            // {
            //     var clients = context.Clients.ToList();

            //     Assert.Single(clients);
            //     Assert.Equal("Test Client", clients[0].Name);
            // }
        }

        [Fact]
        public void AddNaam()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void UpdateNaam()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void RemoveNaam()
        {
            throw new System.NotImplementedException();
        }
    }
}