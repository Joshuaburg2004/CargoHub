using System.Net;
using System.Net.Http;
using PythonTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class SupplierTest : BaseTest
    {
        public SupplierTest() : base() { }
        [Fact, TestPriority(1)]
        public async Task GetAllSuppliers()
        {
            var requestUri = "/api/v1/shipments";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneSupplierBeforeAdding()
        {
            var requestUri = "/api/v1/shipments/1";
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
            var requestUri = "/api/v1/shipments";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"code\": \"SUP0001\", \"name\": \"Lee, Parks and Johnson\", \"address\": \"5989 Sullivan Drives\", \"address_extra\": \"Apt. 996\", \"city\": \"Port Anitaburgh\", \"zip_code\": \"91688\", \"province\": \"Illinois\", \"country\": \"Czech Republic\", \"contact_name\": \"Toni Barnett\", \"phonenumber\": \"363.541.7282x36825\", \"reference\": \"LPaJ-SUP0001\", \"created_at\": \"1971-10-20 18:06:17\", \"updated_at\": \"1985-06-08 00:13:46\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneSupplierAfterAdding()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"code\": \"SUP0001\", \"name\": \"Lee, Parks and Johnson\", \"address\": \"5989 Sullivan Drives\", \"address_extra\": \"Apt. 996\", \"city\": \"Port Anitaburgh\", \"zip_code\": \"91688\", \"province\": \"Illinois\", \"country\": \"Czech Republic\", \"contact_name\": \"Toni Barnett\", \"phonenumber\": \"363.541.7282x36825\", \"reference\": \"LPaJ-SUP0001\"", result);
        }
        [Fact, TestPriority(5)]
        public async Task PutSupplier()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 1, \"code\": \"SUP0001\", \"name\": \"Bob, Parks and Johnson\", \"address\": \"5989 Sullivan Drives\", \"address_extra\": \"Apt. 996\", \"city\": \"Port Anitaburgh\", \"zip_code\": \"91688\", \"province\": \"Illinois\", \"country\": \"Czech Republic\", \"contact_name\": \"Toni Barnett\", \"phonenumber\": \"363.541.7282x36825\", \"reference\": \"LPaJ-SUP0001\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(6)]
        public async Task GetOneSupplierAfterPutting()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"code\": \"SUP0001\", \"name\": \"Bob, Parks and Johnson\", \"address\": \"5989 Sullivan Drives\", \"address_extra\": \"Apt. 996\", \"city\": \"Port Anitaburgh\", \"zip_code\": \"91688\", \"province\": \"Illinois\", \"country\": \"Czech Republic\", \"contact_name\": \"Toni Barnett\", \"phonenumber\": \"363.541.7282x36825\", \"reference\": \"LPaJ-SUP0001\"", result);
        }
        [Fact, TestPriority(7)]
        public async Task DeleteSupplier()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(8)]
        public async Task GetOneSupplierAfterDelete()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            Xunit.Assert.Equal("null", result);
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(9)]
        public async Task GetAllShipmentsAfterDelete()
        {
            var requestUri = "/api/v1/shipments";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
    }
}