using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV1;

namespace MyUnitTestProject
{
    public class ClientServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ClientServiceUnitTest()
        {
            options = new DbContextOptionsBuilder<CargoHubContext>()
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
                    ContactEmail = "testemail@hr.nl"
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetOneNaam()
        {
            using (var context = new CargoHubContext(options))
            {
                var clients = context.Clients.ToList();

                Assert.Single(clients);
                Assert.Equal("Test Client", clients[0].Name);
            }
        }

        [Fact]
        public async void AddNaam()
        {
            using (var context = new CargoHubContext(options))
            {
                var clientService = new ClientServiceV1(context);
                await clientService.AddClient(new Client
                {
                    Id = 2,
                    Name = "Test Client 2",
                    Address = "123 Test St",
                    City = "Test City",
                    ZipCode = "12345",
                    Province = "Test Province",
                    Country = "Test Country",
                    ContactName = "Test Contact",
                    ContactPhone = "123-456-7890",
                    ContactEmail = "testemail@hr.nl",
                });
            }

            using (var context = new CargoHubContext(options))
            {
                var clients = context.Clients.ToList();

                Assert.Equal(2, clients.Count);
                Assert.Equal("Test Client 2", clients[1].Name);
                Assert.Equal("123 Test St", clients[1].Address);
                Assert.Equal("Test City", clients[1].City);
                Assert.Equal("12345", clients[1].ZipCode);
                Assert.Equal("Test Province", clients[1].Province);
                Assert.Equal("Test Country", clients[1].Country);
                Assert.Equal("Test Contact", clients[1].ContactName);
                Assert.Equal("123-456-7890", clients[1].ContactPhone);
                Assert.Equal("testemail@hr.nl", clients[1].ContactEmail);
            }
        }

        [Fact]
        public async void UpdateNaam()
        {
            using (var context = new CargoHubContext(options))
            {
                var clientService = new ClientServiceV1(context);
                var client = context.Clients.First();
                client.Name = "Updated Client";
                await clientService.UpdateClient(client.Id, client);
            }

            using (var context = new CargoHubContext(options))
            {
                var clients = context.Clients.ToList();

                Assert.Single(clients);
                Assert.Equal("Updated Client", clients[0].Name);
                Assert.Equal("123 Test St", clients[0].Address);
                Assert.Equal("Test City", clients[0].City);
                Assert.Equal("12345", clients[0].ZipCode);
                Assert.Equal("Test Province", clients[0].Province);
                Assert.Equal("Test Country", clients[0].Country);
                Assert.Equal("Test Contact", clients[0].ContactName);
                Assert.Equal("123-456-7890", clients[0].ContactPhone);
                Assert.Equal("testemail@hr.nl", clients[0].ContactEmail);
            }
        }

        [Fact]
        public async void RemoveNaam()
        {
            using (var context = new CargoHubContext(options))
            {
                var clientService = new ClientServiceV1(context);
                var client = context.Clients.First();
                await clientService.RemoveClient(client.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var clients = context.Clients.ToList();

                Assert.Empty(clients);
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
