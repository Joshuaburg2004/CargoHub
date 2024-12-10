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
    public class TransferServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public TransferServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Transfers.Add(new Transfer
                {
                    Id = 1,
                    Reference = "Test Reference",
                    TransferFrom = 1,
                    TransferTo = 2,
                    TransferStatus = "Test Status",
                    Items = new List<TransferItem>
                    {
                        new TransferItem
                        {
                            ItemId = "Test Item",
                            Amount = 1
                        }
                    }
                });
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneTransfer()
        {
            using (var context = new CargoHubContext(options))
            {
                var TransferService = new TransferServiceV1(context);
                var Transfers = await TransferService.GetTransfers();

                Assert.NotNull(Transfers);
                Assert.Single(Transfers);
                Assert.Equal("Test Reference", Transfers[0].Reference);
                Assert.Equal(1, Transfers[0].TransferFrom);
                Assert.Equal(2, Transfers[0].TransferTo);
                Assert.Equal("Test Status", Transfers[0].TransferStatus);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddTransfer()
        {
            using (var context = new CargoHubContext(options))
            {
                var TransferService = new TransferServiceV1(context);
                await TransferService.AddTransfer(new Transfer
                {
                    Id = 2,
                    Reference = "Test Transfer 2",
                    TransferFrom = 2,
                    TransferTo = 1,
                    TransferStatus = "Test Status 2",
                    Items = new List<TransferItem>
                    {
                        new TransferItem
                        {
                            ItemId = "Test Item 2",
                            Amount = 2
                        }
                    }
                });
            }

            using (var context = new CargoHubContext(options))
            {
                var Transfers = context.Transfers.ToList();

                Assert.Equal(2, Transfers.Count);
                Assert.Equal("Test Transfer 2", Transfers[1].Reference);
                Assert.Equal(2, Transfers[1].TransferFrom);
                Assert.Equal(1, Transfers[1].TransferTo);
                Assert.Equal("Test Status 2", Transfers[1].TransferStatus);
            }

        }

        [Fact, TestPriority(2)]
        public async void UpdateTransfer()
        {
            using (var context = new CargoHubContext(options))
            {
                var TransferService = new TransferServiceV1(context);
                var Transfer = context.Transfers.First();
                Transfer.Reference = "Updated Transfer";
                await TransferService.UpdateTransfer(Transfer.Id, Transfer);
            }

            using (var context = new CargoHubContext(options))
            {
                var Transfers = context.Transfers.ToList();

                Assert.Single(Transfers);
                Assert.Equal("Updated Transfer", Transfers[0].Reference);
                Assert.Equal(1, Transfers[0].TransferFrom);
                Assert.Equal(2, Transfers[0].TransferTo);
                Assert.Equal("Test Status", Transfers[0].TransferStatus);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveTransfer()
        {
            using (var context = new CargoHubContext(options))
            {
                var TransferService = new TransferServiceV1(context);
                var Transfer = context.Transfers.First();
                await TransferService.RemoveTransfer(Transfer.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Transfers = context.Transfers.ToList();

                Assert.Empty(Transfers);
            }
        }

        [Fact, TestPriority(4)]
        public async void GetItemsInTransfer()
        {
            using (var context = new CargoHubContext(options))
            {
                var TransferService = new TransferServiceV1(context);
                var Items = await TransferService.GetItemsInTransfer(1);

                Assert.NotNull(Items);
                Assert.Single(Items);
                Assert.Equal("Test Item", Items[0].ItemId);
                Assert.Equal(1, Items[0].Amount);
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