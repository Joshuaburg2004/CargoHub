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
    public class WarehouseTest : BaseTest
    {
        private Warehouse _warehouseCreate = new Warehouse
        {
            Id = 1,
            Code = "YQZZNL56",
            Name = "Heemskerk cargo hub",
            Address = "Karlijndreef 281",
            Zip = "4002 AS",
            City = "Heemskerk",
            Province = "Friesland",
            Country = "NL",
            Contact = new Contact("Fem Keijzer", "(078) 0013363", "blamore@example.net")
        };
        private Warehouse _warehouseUpdate = new Warehouse
        {
            Id = 1,
            Code = "YQZZNL57",
            Name = "Heemskerk cargo hub",
            Address = "Karlijndreef 281",
            Zip = "4002 AS",
            City = "Heemskerk",
            Province = "Friesland",
            Country = "NL",
            Contact = new Contact("Fem Keijzer", "(078) 0013363", "blamore@example.net")
        };

        public WarehouseTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact, TestPriority(1)]
        public async Task GetAllWarehouses()
        {
            var requestUri = "/api/v1/warehouses";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetOneWarehouse()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(3)]
        public async Task GetWarehouseLocations()
        {
            var requestUri = "/api/v1/warehouses/1/locations";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(4)]
        public async Task CreateWarehouse()
        {
            var requestUri = "/api/v1/warehouses";
            var response = await _client.PostAsJsonAsync(requestUri, _warehouseCreate);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetOneWarehouseAfterAdding()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Console.Error.WriteLine(result);
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Warehouse? warehouse = await response.Content.ReadFromJsonAsync<Warehouse>();
            Xunit.Assert.Equal(_warehouseCreate.Id, warehouse.Id);
            Xunit.Assert.Equal(_warehouseCreate.Code, warehouse.Code);
            Xunit.Assert.Equal(_warehouseCreate.Name, warehouse.Name);
            Xunit.Assert.Equal(_warehouseCreate.Address, warehouse.Address);
            Xunit.Assert.Equal(_warehouseCreate.Zip, warehouse.Zip);
            Xunit.Assert.Equal(_warehouseCreate.City, warehouse.City);
            Xunit.Assert.Equal(_warehouseCreate.Province, warehouse.Province);
            Xunit.Assert.Equal(_warehouseCreate.Country, warehouse.Country);
            Xunit.Assert.Equal(_warehouseCreate.Contact.Name, warehouse.Contact.Name);
            Xunit.Assert.Equal(_warehouseCreate.Contact.Phone, warehouse.Contact.Phone);
            Xunit.Assert.Equal(_warehouseCreate.Contact.Email, warehouse.Contact.Email);
        }

        [Fact, TestPriority(6)]
        public async Task PutWarehouse()
        {
            // code is changed from "YQZZNL56" to "YQZZNL57"
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.PutAsJsonAsync(requestUri, _warehouseUpdate);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetOneWarehouseAfterUpdating()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Warehouse? warehouse = await response.Content.ReadFromJsonAsync<Warehouse>();
            Xunit.Assert.Equal(_warehouseUpdate.Id, warehouse.Id);
            Xunit.Assert.Equal(_warehouseUpdate.Code, warehouse.Code);
            Xunit.Assert.Equal(_warehouseUpdate.Name, warehouse.Name);
            Xunit.Assert.Equal(_warehouseUpdate.Address, warehouse.Address);
            Xunit.Assert.Equal(_warehouseUpdate.Zip, warehouse.Zip);
            Xunit.Assert.Equal(_warehouseUpdate.City, warehouse.City);
            Xunit.Assert.Equal(_warehouseUpdate.Province, warehouse.Province);
            Xunit.Assert.Equal(_warehouseUpdate.Country, warehouse.Country);
            Xunit.Assert.Equal(_warehouseUpdate.Contact.Name, warehouse.Contact.Name);
            Xunit.Assert.Equal(_warehouseUpdate.Contact.Phone, warehouse.Contact.Phone);
            Xunit.Assert.Equal(_warehouseUpdate.Contact.Email, warehouse.Contact.Email);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteWarehouse()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(9)]
        public async Task GetOneWarehouseAfterDelete()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            // No data found, but Status Code is 200 OK => should be 404 Not Found
        }

        [Fact, TestPriority(10)]
        public async Task GetWarehouseLocationsAfterDelete()
        {
            var requestUri = "/api/v1/warehouses/1/locations";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
            // No data found, but Status Code is 200 OK => should be 404 Not Found
        }

        [Fact, TestPriority(11)]
        public async Task GetWarehouseIdNegative()
        {
            var requestUri = "/api/v1/warehouses/-1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(12)]
        public async Task UpdateWarehouseIdNegative()
        {
            var requestUri = "/api/v1/warehouses/-1";
            var response = await _client.PutAsJsonAsync(requestUri, _warehouseUpdate);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(13)]
        public async Task DeleteWarehouseIdNegative()
        {
            var requestUri = "/api/v1/warehouses/-1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}