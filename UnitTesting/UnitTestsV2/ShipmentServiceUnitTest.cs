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
    public class ShipmentServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public ShipmentServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Shipments.Add(new Shipment
                {
                    Id = 1,
                    OrderId = 1,
                    SourceId = 1,
                    OrderDate = "2021-01-01",
                    RequestDate = "2021-01-02",
                    ShipmentDate = "2021-01-03",
                    ShipmentType = "Test Type",
                    ShipmentStatus = "Test Status",
                    Notes = "Test Notes",
                    CarrierCode = "Test Carrier Code",
                    CarrierDescription = "Test Carrier Description",
                    ServiceCode = "Test Service Code",
                    PaymentType = "Test Payment Type",
                    TransferMode = "Test Transfer Mode",
                    TotalPackageCount = 1,
                    TotalPackageWeight = 1.0,
                    Items = new List<ShipmentItem>
                    {
                        new ShipmentItem
                        {
                            ItemId = "P000001",
                            Amount = 1,
                        }
                    },

                });
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetOneShipment()
        {
            using (var context = new CargoHubContext(options))
            {
                var ShipmentService = new ShipmentServiceV2(context);
                var Shipments = await ShipmentService.GetAllShipments();

                Assert.NotNull(Shipments);
                Assert.Single(Shipments);
                Assert.Equal(1, Shipments[0].Id);
                Assert.Equal(1, Shipments[0].OrderId);
                Assert.Equal(1, Shipments[0].SourceId);
                Assert.Equal("2021-01-01", Shipments[0].OrderDate);
                Assert.Equal("2021-01-02", Shipments[0].RequestDate);
                Assert.Equal("2021-01-03", Shipments[0].ShipmentDate);
                Assert.Equal("Test Type", Shipments[0].ShipmentType);
                Assert.Equal("Test Status", Shipments[0].ShipmentStatus);
                Assert.Equal("Test Notes", Shipments[0].Notes);
                Assert.Equal("Test Carrier Code", Shipments[0].CarrierCode);
                Assert.Equal("Test Carrier Description", Shipments[0].CarrierDescription);
                Assert.Equal("Test Service Code", Shipments[0].ServiceCode);
                Assert.Equal("Test Payment Type", Shipments[0].PaymentType);
                Assert.Equal("Test Transfer Mode", Shipments[0].TransferMode);
                Assert.Equal(1, Shipments[0].TotalPackageCount);
                Assert.Equal(1.0, Shipments[0].TotalPackageWeight);
                Assert.Equal("P000001", Shipments[0].Items[0].ItemId);
                Assert.Equal(1, Shipments[0].Items[0].Amount);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddShipment()
        {
            using (var context = new CargoHubContext(options))
            {
                var ShipmentService = new ShipmentServiceV2(context);
                await ShipmentService.AddShipment(new Shipment
                {
                    Id = 2,
                    OrderId = 2,
                    SourceId = 2,
                    OrderDate = "2021-01-01",
                    RequestDate = "2021-01-02",
                    ShipmentDate = "2021-01-03",
                    ShipmentType = "Test Type 2",
                    ShipmentStatus = "Test Status 2",
                    Notes = "Test Notes 2",
                    CarrierCode = "Test Carrier Code 2",
                    CarrierDescription = "Test Carrier Description 2",
                    ServiceCode = "Test Service Code 2",
                    PaymentType = "Test Payment Type 2",
                    TransferMode = "Test Transfer Mode 2",
                    TotalPackageCount = 2,
                    TotalPackageWeight = 2.0,
                    Items = new List<ShipmentItem>
                    {
                        new ShipmentItem
                        {
                            ItemId = "P000002",
                            Amount = 2,
                        }
                    },
                });
            }

            using (var context = new CargoHubContext(options))
            {
                var Shipments = context.Shipments.ToList();

                Assert.Equal(2, Shipments.Count);
                Assert.Equal(2, Shipments[1].Id);
                Assert.Equal(2, Shipments[1].OrderId);
                Assert.Equal(2, Shipments[1].SourceId);
                Assert.Equal("2021-01-01", Shipments[1].OrderDate);
                Assert.Equal("2021-01-02", Shipments[1].RequestDate);
                Assert.Equal("2021-01-03", Shipments[1].ShipmentDate);
                Assert.Equal("Test Type 2", Shipments[1].ShipmentType);
                Assert.Equal("Test Status 2", Shipments[1].ShipmentStatus);
                Assert.Equal("Test Notes 2", Shipments[1].Notes);
                Assert.Equal("Test Carrier Code 2", Shipments[1].CarrierCode);
                Assert.Equal("Test Carrier Description 2", Shipments[1].CarrierDescription);
                Assert.Equal("Test Service Code 2", Shipments[1].ServiceCode);
                Assert.Equal("Test Payment Type 2", Shipments[1].PaymentType);
                Assert.Equal("Test Transfer Mode 2", Shipments[1].TransferMode);
                Assert.Equal(2, Shipments[1].TotalPackageCount);
                Assert.Equal(2.0, Shipments[1].TotalPackageWeight);
                Assert.Equal("P000002", Shipments[1].Items[0].ItemId);
                Assert.Equal(2, Shipments[1].Items[0].Amount);
            }

        }

        [Fact, TestPriority(2)]
        public async void UpdateShipment()
        {
            using (var context = new CargoHubContext(options))
            {
                var ShipmentService = new ShipmentServiceV2(context);
                var Shipment = context.Shipments.First();
                Shipment.Notes = "Updated Shipment";
                await ShipmentService.UpdateShipment(Shipment.Id, Shipment);
            }

            using (var context = new CargoHubContext(options))
            {
                var Shipments = context.Shipments.ToList();

                Assert.Single(Shipments);
                Assert.Equal(1, Shipments[0].Id);
                Assert.Equal(1, Shipments[0].OrderId);
                Assert.Equal(1, Shipments[0].SourceId);
                Assert.Equal("Updated Shipment", Shipments[0].Notes);
                Assert.Equal("2021-01-01", Shipments[0].OrderDate);
                Assert.Equal("2021-01-02", Shipments[0].RequestDate);
                Assert.Equal("2021-01-03", Shipments[0].ShipmentDate);
                Assert.Equal("Test Type", Shipments[0].ShipmentType);
                Assert.Equal("Test Status", Shipments[0].ShipmentStatus);
                Assert.Equal("Test Carrier Code", Shipments[0].CarrierCode);
                Assert.Equal("Test Carrier Description", Shipments[0].CarrierDescription);
                Assert.Equal("Test Service Code", Shipments[0].ServiceCode);
                Assert.Equal("Test Payment Type", Shipments[0].PaymentType);
                Assert.Equal("Test Transfer Mode", Shipments[0].TransferMode);
                Assert.Equal(1, Shipments[0].TotalPackageCount);
                Assert.Equal(1.0, Shipments[0].TotalPackageWeight);
                Assert.Equal("P000001", Shipments[0].Items[0].ItemId);
                Assert.Equal(1, Shipments[0].Items[0].Amount);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveShipment()
        {
            using (var context = new CargoHubContext(options))
            {
                var ShipmentService = new ShipmentServiceV2(context);
                var Shipment = context.Shipments.First();
                await ShipmentService.DeleteShipment(Shipment.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Shipments = context.Shipments.ToList();

                Assert.Empty(Shipments);
            }
        }

        [Fact, TestPriority(3)]
        public async void GetItemsfromShipmentById()
        {
            using (var context = new CargoHubContext(options))
            {
                var ShipmentService = new ShipmentServiceV2(context);
                var Items = await ShipmentService.GetItemsfromShipmentById(1);

                Assert.NotNull(Items);
                Assert.Single(Items);
                Assert.Equal("P000001", Items[0].ItemId);
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