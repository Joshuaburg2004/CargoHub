using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CargoHubAlt.Models;
using CargoHubAlt.Services.ServicesV1;
using CargoHubAlt.Database;

namespace CargoHub.UnitTesting
{
    [TestCaseOrderer("UnitTests.PriorityOrderer", "UnitTests")]
    public class SupplierServiceUnitTest : IDisposable
    {
        private readonly DbContextOptions<CargoHubContext> options;
        public SupplierServiceUnitTest()
        {
            var options = new DbContextOptionsBuilder<CargoHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new CargoHubContext(options))
            {
                context.Suppliers.Add(new Supplier
                {
                    Id = 1,
                    Code = "abc123",
                    Name = "Test Supplier",
                    Address = "123 Test St",
                    AddressExtra = "Suite 101",
                    City = "Test City",
                    ZipCode = "12345",
                    Province = "Test Province",
                    Country = "Test Country",
                    ContactName = "Test Contact",
                    Phonenumber = "123-456-7890",
                    Reference = "Test Reference"
                });
                context.Suppliers.Add(new Supplier
                {
                    Id = 2,
                    Code = "def456",
                    Name = "Test Supplier 2",
                    Address = "456 Test",
                    AddressExtra = "Suite 202",
                    City = "Test City 2",
                    ZipCode = "54321",
                    Province = "Test Province 2",
                    Country = "Test Country 2",
                    ContactName = "Test Contact 2",
                    Phonenumber = "098-765-4321",
                    Reference = "Test Reference 2"
                });
                context.SaveChanges();
            }

            this.options = options;
        }

        [Fact, TestPriority(0)]
        public async void GetAllSuppliers()
        {
            using (var context = new CargoHubContext(options))
            {
                var SupplierService = new SupplierServiceV1(context);
                var suppliers = await SupplierService.GetAllSuppliers();

                Assert.Equal(2, suppliers.Count);
                Assert.Equal("abc123", suppliers[0].Code);
                Assert.Equal("Test Supplier", suppliers[0].Name);
                Assert.Equal("123 Test St", suppliers[0].Address);
                Assert.Equal("Suite 101", suppliers[0].AddressExtra);
                Assert.Equal("Test City", suppliers[0].City);
                Assert.Equal("12345", suppliers[0].ZipCode);
                Assert.Equal("Test Province", suppliers[0].Province);
                Assert.Equal("Test Country", suppliers[0].Country);
                Assert.Equal("Test Contact", suppliers[0].ContactName);
                Assert.Equal("123-456-7890", suppliers[0].Phonenumber);
                Assert.Equal("Test Reference", suppliers[0].Reference);
            }
        }

        [Fact, TestPriority(0)]
        public async void GetOneSupplier()
        {
            using (var context = new CargoHubContext(options))
            {
                var SupplierService = new SupplierServiceV1(context);
                var supplier = await SupplierService.GetSupplier(1);

                Assert.NotNull(supplier);
                Assert.Equal("abc123", supplier.Code);
                Assert.Equal("Test Supplier", supplier.Name);
                Assert.Equal("123 Test St", supplier.Address);
                Assert.Equal("Suite 101", supplier.AddressExtra);
                Assert.Equal("Test City", supplier.City);
                Assert.Equal("12345", supplier.ZipCode);
                Assert.Equal("Test Province", supplier.Province);
                Assert.Equal("Test Country", supplier.Country);
                Assert.Equal("Test Contact", supplier.ContactName);
                Assert.Equal("123-456-7890", supplier.Phonenumber);
                Assert.Equal("Test Reference", supplier.Reference);
            }
        }

        [Fact, TestPriority(1)]
        public async void AddSupplier()
        {
            using (var context = new CargoHubContext(options))
            {
                var SupplierService = new SupplierServiceV1(context);
                var supplier = new Supplier
                {
                    Id = 3,
                    Code = "ghi789",
                    Name = "Test Supplier 3",
                    Address = "789 Test St",
                    AddressExtra = "Suite 303",
                    City = "Test City 3",
                    ZipCode = "98765",
                    Province = "Test Province 3",
                    Country = "Test Country 3",
                    ContactName = "Test Contact 3",
                    Phonenumber = "123-456-7890",
                    Reference = "Test Reference 3"
                };
                await SupplierService.AddSupplier(supplier);
            }

            using (var context = new CargoHubContext(options))
            {
                var Suppliers = context.Suppliers.ToList();

                Assert.Equal(3, Suppliers.Count);
                Assert.Equal("ghi789", Suppliers[2].Code);
                Assert.Equal("Test Supplier 3", Suppliers[2].Name);
                Assert.Equal("789 Test St", Suppliers[2].Address);
                Assert.Equal("Suite 303", Suppliers[2].AddressExtra);
                Assert.Equal("Test City 3", Suppliers[2].City);
                Assert.Equal("98765", Suppliers[2].ZipCode);
                Assert.Equal("Test Province 3", Suppliers[2].Province);
                Assert.Equal("Test Country 3", Suppliers[2].Country);
                Assert.Equal("Test Contact 3", Suppliers[2].ContactName);
                Assert.Equal("123-456-7890", Suppliers[2].Phonenumber);
                Assert.Equal("Test Reference 3", Suppliers[2].Reference);
            }
        }

        [Fact, TestPriority(2)]
        public async void UpdateSupplier()
        {
            using (var context = new CargoHubContext(options))
            {
                var SupplierService = new SupplierServiceV1(context);
                var Supplier = context.Suppliers.First();
                Supplier.ContactName = "new contact name";
                await SupplierService.UpdateSupplier(Supplier.Id, Supplier);
            }

            using (var context = new CargoHubContext(options))
            {
                var Suppliers = context.Suppliers.ToList();

                Assert.Equal("abc123", Suppliers[0].Code);
                Assert.Equal("Test Supplier", Suppliers[0].Name);
                Assert.Equal("123 Test St", Suppliers[0].Address);
                Assert.Equal("Suite 101", Suppliers[0].AddressExtra);
                Assert.Equal("Test City", Suppliers[0].City);
                Assert.Equal("12345", Suppliers[0].ZipCode);
                Assert.Equal("Test Province", Suppliers[0].Province);
                Assert.Equal("Test Country", Suppliers[0].Country);
                Assert.Equal("new contact name", Suppliers[0].ContactName);
                Assert.Equal("123-456-7890", Suppliers[0].Phonenumber);
                Assert.Equal("Test Reference", Suppliers[0].Reference);
            }
        }

        [Fact, TestPriority(3)]
        public async void RemoveSupplier()
        {
            using (var context = new CargoHubContext(options))
            {
                var SupplierService = new SupplierServiceV1(context);
                var Supplier = context.Suppliers.First();
                await SupplierService.DeleteSupplier(Supplier.Id);
                var Suppliertwo = context.Suppliers.First();
                await SupplierService.DeleteSupplier(Suppliertwo.Id);
            }

            using (var context = new CargoHubContext(options))
            {
                var Suppliers = context.Suppliers.ToList();

                Assert.Empty(Suppliers);
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