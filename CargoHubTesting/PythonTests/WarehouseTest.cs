using System.Net;
using System.Text.Json;
using CargoHubAlt.Models;
using PythonTests.models;

namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class WarehouseTest : BaseTest
    {
        public static Contact testContact = new Contact("Fem Keijzer", "(078) 0013363", "blamore@example.net");
        public static Warehouse testwarehouse = new Warehouse(1, "YQZZNL56", "Heemskerk cargo hub", "Karlijndreef 281", "4002 AS", "Heemskerk", "Friesland", "NL", testContact);
        public static string testTypeJson { get => JsonSerializer.Serialize(testwarehouse); }
        public WarehouseTest() : base() { }

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
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
            var response = await _client.PostAsync(requestUri, new StringContent(testTypeJson, System.Text.Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetOneWarehouseAfterAdding()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.GetAsync(requestUri);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Warehouse? ToCompare = JsonSerializer.Deserialize<Warehouse>(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(testwarehouse.Id, ToCompare.Id);
            Xunit.Assert.Equal(testwarehouse.Code, ToCompare.Code);
            Xunit.Assert.Equal(testwarehouse.Name, ToCompare.Name);
            Xunit.Assert.Equal(testwarehouse.Address, ToCompare.Address);
            Xunit.Assert.Equal(testwarehouse.Zip, ToCompare.Zip);
            Xunit.Assert.Equal(testwarehouse.City, ToCompare.City);
            Xunit.Assert.Equal(testwarehouse.Province, ToCompare.Province);
            Xunit.Assert.Equal(testwarehouse.Country, ToCompare.Country);
            Xunit.Assert.Equal(testwarehouse.Contact.Name, ToCompare.Contact.Name);
            Xunit.Assert.Equal(testwarehouse.Contact.Phone, ToCompare.Contact.Phone);
            Xunit.Assert.Equal(testwarehouse.Contact.Email, ToCompare.Contact.Email);
        }

        [Fact, TestPriority(6)]
        public async Task PutWarehouse()
        {
            var requestUri = "/api/v1/warehouses/1";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 1, \"code\": \"YQZZNL57\", \"name\": \"Heemskerk cargo hub\", \"address\": \"Karlijndreef 281\", \"zip\": \"4002 AS\", \"city\": \"Heemskerk\", \"province\": \"Friesland\", \"country\": \"NL\", \"contact\": {\"name\": \"Fem Keijzer\", \"phone\": \"(078) 0013363\", \"email\": \"blamore@example.net\"}, \"created_at\": \"1983-04-13 04:59:55\", \"updated_at\": \"2007-02-08 20:11:00\"}", System.Text.Encoding.UTF8, "application/json"));
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
            Xunit.Assert.Contains("{\"id\":1,\"code\":\"YQZZNL57\",\"name\":\"Heemskerk cargo hub\",\"address\":\"Karlijndreef 281\",\"zip\":\"4002 AS\",\"city\":\"Heemskerk\",\"province\":\"Friesland\",\"country\":\"NL\",\"contact\":{\"name\":\"Fem Keijzer\",\"phone\":\"(078) 0013363\",\"email\":\"blamore@example.net\"},\"created_at\":\"1983-04-13 04:59:55\",\"updated_at\":\"2007-02-08 20:11:00\"}", result);
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
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("", result);
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
    }
}