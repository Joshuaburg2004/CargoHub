using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services;
using CargoHubAlt.Database;
using CargoHubAlt.Services.ServicesV1;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class ClientServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ClientServiceUnitTest()
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
                    ContactEmail = "test@hr.nl"
                });
                context.SaveChanges();
            }


            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneClient()
        {
            using (var context = new CargoHubContext(options))
            {
                var ClientService = new ClientServiceV1(context);
                var Clients = await ClientService.GetAllClients();

                Assert.NotNull(Clients);
                Assert.Single(Clients);
                Assert.Equal("Test Client", Clients[0].Name);
                Assert.Equal("123 Test St", Clients[0].Address);
                Assert.Equal("Test City", Clients[0].City);
                Assert.Equal("12345", Clients[0].ZipCode);
                Assert.Equal("Test Province", Clients[0].Province);
                Assert.Equal("Test Country", Clients[0].Country);
                Assert.Equal("Test Contact", Clients[0].ContactName);
                Assert.Equal("123-456-7890", Clients[0].ContactPhone);
                Assert.Equal("test@hr.nl", Clients[0].ContactEmail);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddClient()
        {
            using (var context = new CargoHubContext(options))
            {
                var ClientService = new ClientServiceV1(context);
                var Client = new Client
                {
                    Name = "Test Client 2",
                    Address = "123 Test St",
                    City = "Test City",
                    ZipCode = "12345",
                    Province = "Test Province",
                    Country = "Test Country",
                    ContactName = "Test Contact",
                    ContactPhone = "123-456-7890",
                    ContactEmail = "test2@hr.nl"
                };

                await ClientService.AddClient(Client);
                var Clients = await ClientService.GetAllClients();

                Assert.NotNull(Clients);
                Assert.Equal(2, Clients.Count);
                Assert.Equal("Test Client 2", Clients[1].Name);
                Assert.Equal("123 Test St", Clients[1].Address);
                Assert.Equal("Test City", Clients[1].City);
                Assert.Equal("12345", Clients[1].ZipCode);
                Assert.Equal("Test Province", Clients[1].Province);
                Assert.Equal("Test Country", Clients[1].Country);
                Assert.Equal("Test Contact", Clients[1].ContactName);
                Assert.Equal("123-456-7890", Clients[1].ContactPhone);
                Assert.Equal("test2@hr.nl", Clients[1].ContactEmail);
            }
        }

        [Fact, TestPriority(2)]
        public async void UpdateClient()
        {
            using (var context = new CargoHubContext(options))
            {
                var ClientService = new ClientServiceV1(context);
                var Client = context.Clients.First();
                Client.Name = "Updated Client";
                await ClientService.UpdateClient(Client.Id, Client);
            }

            using (var context = new CargoHubContext(options))
            {
                var Clients = context.Clients.ToList();

                Assert.Single(Clients);
                Assert.Equal("Updated Client", Clients[0].Name);
                Assert.Equal("123 Test St", Clients[0].Address);
                Assert.Equal("Test City", Clients[0].City);
                Assert.Equal("12345", Clients[0].ZipCode);
                Assert.Equal("Test Province", Clients[0].Province);
                Assert.Equal("Test Country", Clients[0].Country);
                Assert.Equal("Test Contact", Clients[0].ContactName);
                Assert.Equal("123-456-7890", Clients[0].ContactPhone);
                Assert.Equal("test@hr.nl", Clients[0].ContactEmail);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveClient()
        {
            using (var context = new CargoHubContext(options))
            {
                var ClientService = new ClientServiceV1(context);
                var Client = context.Clients.First();
                await ClientService.RemoveClient(Client.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Clients = context.Clients.ToList();

                Assert.Empty(Clients);
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