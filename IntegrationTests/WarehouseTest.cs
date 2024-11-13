using System.Net;
using System.Text.Json;

namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class WarehouseTest : BaseTest
    {
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
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"code\": \"YQZZNL56\", \"name\": \"Heemskerk cargo hub\", \"address\": \"Karlijndreef 281\", \"zip\": \"4002 AS\", \"city\": \"Heemskerk\", \"province\": \"Friesland\", \"country\": \"NL\", \"contact\": {\"name\": \"Fem Keijzer\", \"phone\": \"(078) 0013363\", \"email\": \"blamore@example.net\"}, \"created_at\": \"1983-04-13 04:59:55\", \"updated_at\": \"2007-02-08 20:11:00\"}", System.Text.Encoding.UTF8, "application/json"));
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
            var jsonResponse = JsonDocument.Parse(result).RootElement;
            Xunit.Assert.Equal("YQZZNL56", jsonResponse.GetProperty("code").GetString());
            Xunit.Assert.Equal("Heemskerk cargo hub", jsonResponse.GetProperty("name").GetString());
            Xunit.Assert.Equal("Karlijndreef 281", jsonResponse.GetProperty("address").GetString());
            Xunit.Assert.Equal("4002 AS", jsonResponse.GetProperty("zip").GetString());
            Xunit.Assert.Equal("Heemskerk", jsonResponse.GetProperty("city").GetString());
            Xunit.Assert.Equal("Friesland", jsonResponse.GetProperty("province").GetString());
            Xunit.Assert.Equal("NL", jsonResponse.GetProperty("country").GetString());
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
            var jsonResponse = JsonDocument.Parse(result).RootElement;
            Xunit.Assert.Equal("YQZZNL57", jsonResponse.GetProperty("code").GetString());
            Xunit.Assert.Equal("Heemskerk cargo hub", jsonResponse.GetProperty("name").GetString());
            Xunit.Assert.Equal("Karlijndreef 281", jsonResponse.GetProperty("address").GetString());
            Xunit.Assert.Equal("4002 AS", jsonResponse.GetProperty("zip").GetString());
            Xunit.Assert.Equal("Heemskerk", jsonResponse.GetProperty("city").GetString());
            Xunit.Assert.Equal("Friesland", jsonResponse.GetProperty("province").GetString());
            Xunit.Assert.Equal("NL", jsonResponse.GetProperty("country").GetString());
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