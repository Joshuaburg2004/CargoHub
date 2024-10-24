using System.Net;
using System.Net.Http;
using PythonTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
namespace PythonTests{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class LocationTest : BaseTest {
        public LocationTest() : base(){}
        [Fact, TestPriority(1)]
        public async Task GetAllLocations()
        {
            var requestUri = "/api/v1/locations";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneLocationBeforeAdding(){
            var requestUri = "/api/v1/locations/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("null", result);
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is idiotic.
        }
        [Fact, TestPriority(3)]
        public async Task CreateLocation()
        {
            var requestUri = "/api/v1/locations";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"warehouse_id\": 1, \"code\": \"A.1.0\", \"name\": \"Row: A, Rack: 1, Shelf: 0\", \"created_at\": \"1992-05-15 03:21:32\", \"updated_at\": \"1992-05-15 03:21:32\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneLocationAfterAdding(){
            var requestUri = "/api/v1/locations/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"warehouse_id\": 1, \"code\": \"A.1.0\", \"name\": \"Row: A, Rack: 1, Shelf: 0\"",  result);
        }
    }
}