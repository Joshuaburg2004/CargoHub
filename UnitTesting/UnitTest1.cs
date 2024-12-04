using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services;
using CargoHubAlt.Database;
public class UnitTest1
{
    [Fact]
    public void TestMethod()
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


        using (var context = new CargoHubContext(options))
        {
            var clients = context.Clients.ToList();

            // Assert
            Assert.Single(clients);
            Assert.Equal("Test Client", clients[0].Name);
        }
    }
}