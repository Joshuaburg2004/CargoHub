using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using CargoHubAlt.Models;
using System.Net.Http.Json;
namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class SupplierTest : BaseTest
    {
        private Supplier _newsupplier = new Supplier(1, "SUP0001", "Lee, Parks and Johnson", "5989 Sullivan Drives", "Apt. 996", "Port Anitaburgh", "91688", "Illinois", "Czech Republic", "Toni Barnett", "363.541.7282x36825", "LPaJ-SUP0001");
        private Supplier _supplierToPut = new Supplier(1, "SUP0001", "Bob, Parks and Johnson", "5989 Sullivan Drives", "Apt. 996", "Port Anitaburgh", "91688", "Illinois", "Czech Republic", "Toni Barnett", "363.541.7282x36825", "LPaJ-SUP0001");
        public SupplierTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
        [Fact, TestPriority(1)]
        public async Task GetAllSuppliers()
        {
            var requestUri = "/api/v1/suppliers";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneSupplierBeforeAdding()
        {
            var requestUri = "/api/v1/suppliers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(3)]
        public async Task CreateSupplier()
        {
            var requestUri = "/api/v1/suppliers";
            var response = await _client.PostAsJsonAsync(requestUri, _newsupplier);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneSupplierAfterAdding()
        {
            var requestUri = "/api/v1/suppliers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Supplier? supplier = await response.Content.ReadFromJsonAsync<Supplier>();
            
            Xunit.Assert.NotNull(supplier);
            Xunit.Assert.Equal(_newsupplier.Id, supplier.Id);
            Xunit.Assert.Equal(_newsupplier.Code, supplier.Code);
            Xunit.Assert.Equal(_newsupplier.Name, supplier.Name);
            Xunit.Assert.Equal(_newsupplier.Address, supplier.Address);
            Xunit.Assert.Equal(_newsupplier.AddressExtra, supplier.AddressExtra);
            Xunit.Assert.Equal(_newsupplier.City, supplier.City);
            Xunit.Assert.Equal(_newsupplier.ZipCode, supplier.ZipCode);
            Xunit.Assert.Equal(_newsupplier.Province, supplier.Province);
            Xunit.Assert.Equal(_newsupplier.Country, supplier.Country);
            Xunit.Assert.Equal(_newsupplier.ContactName, supplier.ContactName);
            Xunit.Assert.Equal(_newsupplier.Phonenumber, supplier.Phonenumber);
            Xunit.Assert.Equal(_newsupplier.Reference, supplier.Reference);

            }
        [Fact, TestPriority(5)]
        public async Task PutSupplier()
        {
            var requestUri = "/api/v1/suppliers/1";
            var response = await _client.PutAsJsonAsync(requestUri, _supplierToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(6)]
        public async Task GetOneSupplierAfterPutting()
        {
            var requestUri = "/api/v1/suppliers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Supplier? supplier = await response.Content.ReadFromJsonAsync<Supplier>();

            Xunit.Assert.NotNull(supplier);
            Xunit.Assert.Equal(_supplierToPut.Id, supplier.Id);
            Xunit.Assert.Equal(_supplierToPut.Code, supplier.Code);
            Xunit.Assert.Equal(_supplierToPut.Name, supplier.Name);
            Xunit.Assert.Equal(_supplierToPut.Address, supplier.Address);
            Xunit.Assert.Equal(_supplierToPut.AddressExtra, supplier.AddressExtra);
            Xunit.Assert.Equal(_supplierToPut.City, supplier.City);
            Xunit.Assert.Equal(_supplierToPut.ZipCode, supplier.ZipCode);
            Xunit.Assert.Equal(_supplierToPut.Province, supplier.Province);
            Xunit.Assert.Equal(_supplierToPut.Country, supplier.Country);
            Xunit.Assert.Equal(_supplierToPut.ContactName, supplier.ContactName);
            Xunit.Assert.Equal(_supplierToPut.Phonenumber, supplier.Phonenumber);
            Xunit.Assert.Equal(_supplierToPut.Reference, supplier.Reference);
        }
        [Fact, TestPriority(7)]
        public async Task DeleteSupplier()
        {
            var requestUri = "/api/v1/suppliers/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(8)]
        public async Task GetOneSupplierAfterDelete()
        {
            var requestUri = "/api/v1/suppliers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(9)]
        public async Task GetAllShipmentsAfterDelete()
        {
            var requestUri = "/api/v1/suppliers";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
    }
}