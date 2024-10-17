using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;

namespace MyTests
{
    [TestCaseOrderer("MyTests.PriorityOrderer", "CargoHubTesting")]

    public class LocationIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid location1Id;
        public static Location locationafterupdate;

        public LocationIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllLocationsEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/locations/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateLocation()
        {
            var response = await _client.PostAsync("/api/v1/locations", new StringContent("{\"Code\":\"LOC123\",\"Name\":\"Location Name\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", Encoding.UTF8, "application/json"));
            Console.Error.WriteLine(response.Content.ReadAsStringAsync().Result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var locationid = JsonSerializer.Deserialize<Guid>(responseContent);

            Xunit.Assert.NotEqual(Guid.Empty, locationid);
            location1Id = locationid;
        }

        [Fact, TestPriority(2)]
        public async Task GetAllLocations()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/locations/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEqual("[]", responseContent);
        }

        // [Fact, TestPriority(3)]
        // public async Task GetLocationById()
        // {
        //     HttpResponseMessage response = await _client.GetAsync($"/api/v1/locations?id={location1Id}");
        //     Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //     var responseContent = await response.Content.ReadAsStringAsync();
        //     var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //     var location = JsonSerializer.Deserialize<Location>(responseContent, options);

        //     Xunit.Assert.NotNull(location);
        //     Xunit.Assert.Equal(location1Id, location.Id);
        // }

        // [Fact, TestPriority(4)]
        // public async Task UpdateLocation()
        // {
        //     HttpResponseMessage response = await _client.PutAsync($"/api/v1/locations?id={location1Id}", new StringContent("{\"Code\":\"LOC123\",\"Name\":\"Location Name Updated\",\"Address\":\"123 Main St\",\"AddressExtra\":\"Suite 100\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"PhoneNumber\":\"+31 10 123 4567\",\"Reference\":\"REF123\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
        //     Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //     var responseContent = await response.Content.ReadAsStringAsync();
        //     var location = JsonSerializer.Deserialize<Location>(responseContent);

        //     Xunit.Assert.NotNull(location);
        //     Xunit.Assert.Equal(location1Id, location.Id);
        //     Xunit.Assert.Equal("Location Name Updated", location.Name);
        //     locationafterupdate = location;
        // }

        // [Fact, TestPriority(5)]
        // public async Task GetLoactionByIdAfterUpdate()
        // {
        //     HttpResponseMessage response = await _client.GetAsync($"/api/v1/locations?id={location1Id}");
        //     Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //     var responseContent = await response.Content.ReadAsStringAsync();
        //     var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //     var location = JsonSerializer.Deserialize<Location>(responseContent, options);

        //     Xunit.Assert.NotNull(location);
        //     Xunit.Assert.Equal(location1Id, location.Id);
        //     Xunit.Assert.Equal(locationafterupdate.Name, location.Name);
        // }

        // [Fact, TestPriority(6)]
        // public async Task DeleteLocation()
        // {
        //     HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/locations?id={location1Id}");
        //     Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // }

        // [Fact, TestPriority(7)]
        // public async Task GetAllLoactionssAfterDelete()
        // {
        //     HttpResponseMessage response = await _client.GetAsync("/api/v1/locations/getall");
        //     Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //     Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        // }
    }
}