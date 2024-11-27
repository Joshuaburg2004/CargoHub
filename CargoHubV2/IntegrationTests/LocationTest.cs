using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;

using CargoHubAlt.Models;
using System.Text.Json;
using System.Net.Http.Json;
namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class LocationTest : BaseTest
    {
        public static Location initialLoc = new(1, 1, "A.1.0", "new");
        public static Location afterPutLoc = new(1, 1, "B.2.1", "afterput");

        public LocationTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
        [Fact, TestPriority(1)]
        public async Task GetAllLocations()
        {
            var requestUri = "/api/v2/locations";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneLocationBeforeAdding()
        {
            var requestUri = "/api/v2/locations/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }
        [Fact, TestPriority(3)]
        public async Task CreateLocation()
        {
            var requestUri = "/api/v2/locations";
            var response = await _client.PostAsJsonAsync(requestUri, initialLoc);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneLocationAfterAdding()
        {
            var requestUri = "/api/v2/locations/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Location? found = await response.Content.ReadFromJsonAsync<Location>();

            Xunit.Assert.IsType<Location>(found);
            Xunit.Assert.Equal(initialLoc.Id, found.Id);
            Xunit.Assert.Equal(initialLoc.WarehouseId, found.WarehouseId);
            Xunit.Assert.Equal(initialLoc.Code, found.Code);
            Xunit.Assert.Equal(initialLoc.Name, found.Name);
            Xunit.Assert.NotNull(found.CreatedAt);
            Xunit.Assert.NotNull(found.UpdatedAt);
        }

        [Fact, TestPriority(5)]
        public async Task PutLocation()
        {
            var requestUri = "/api/v2/locations/1";
            var response = await _client.PutAsJsonAsync(requestUri, afterPutLoc);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(6)]
        public async Task GetOneLocationAfterPutting()
        {
            var requestUri = "/api/v2/locations/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Location? found = await response.Content.ReadFromJsonAsync<Location>();
            Xunit.Assert.IsType<Location>(found);
            Xunit.Assert.Equal(afterPutLoc.Id, found.Id);
            Xunit.Assert.Equal(afterPutLoc.WarehouseId, found.WarehouseId);
            Xunit.Assert.Equal(afterPutLoc.Code, found.Code);
            Xunit.Assert.Equal(afterPutLoc.Name, found.Name);
            Xunit.Assert.NotNull(found.CreatedAt);
            Xunit.Assert.NotNull(found.UpdatedAt);

        }
        [Fact, TestPriority(7)]
        public async Task DeleteLocation()
        {
            var requestUri = "/api/v2/locations/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(8)]
        public async Task GetOneLocationAfterDelete()
        {
            var requestUri = "/api/v2/locations/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(9)]
        public async Task GetAllLocationsAfterDelete()
        {
            var requestUri = "/api/v2/locations";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
    }
}