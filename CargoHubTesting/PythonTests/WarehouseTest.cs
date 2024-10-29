using System.Net;

namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class WarehouseTest : BaseTest
    {
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
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 99, \"name\": \"Summers Logistics LLC\", \"address\": \"2057 Main Street Suite 300\", \"city\": \"Greenville\", \"zip_code\": \"29601\", \"province\": \"South Carolina\", \"country\": \"United States\", \"contact_name\": \"Jordan Smith\", \"contact_phone\": \"(864) 555-0147\", \"contact_email\": \"jordan.smith@example.com\", \"created_at\": \"1990-07-16T08:45:30Z\", \"updated_at\": \"2022-04-12T12:20:45Z\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetOneWarehouseAfterAdding()
        {
            var requestUri = "/api/v1/warehouses/99";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(6)]
        public async Task PutWarehouse()
        {
            var requestUri = "/api/v1/warehouses/99";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 99, \"name\": \"Summers Logistics LLC\", \"address\": \"2057 Main Street Suite 300\", \"city\": \"Greenville\", \"zip_code\": \"29601\", \"province\": \"South Carolina\", \"country\": \"United States\", \"contact_name\": \"John Doe\", \"contact_phone\": \"(864) 555-0147\", \"contact_email\": \"jordan.smith@example.com\", \"created_at\": \"1990-07-16T08:45:30Z\", \"updated_at\": \"2022-04-12T12:20:45Z\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetOneWarehouseAfterUpdating()
        {
            var requestUri = "/api/v1/warehouses/99";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("\"contact_name\": \"John Doe\"", result);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteWarehouse()
        {
            var requestUri = "/api/v1/warehouses/99";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(9)]
        public async Task GetOneWarehouseAfterDelete()
        {
            var requestUri = "/api/v1/warehouses/99";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Xunit.Assert.Equal("null", result);
            // No data found, but Status Code is 200 OK => should be 404 Not Found
        }

        [Fact, TestPriority(10)]
        public async Task GetWarehouseLocationsAfterDelete()
        {
            var requestUri = "/api/v1/warehouses/99/locations";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Xunit.Assert.Equal("null", result);
            // No data found, but Status Code is 200 OK => should be 404 Not Found
        }
    }
}