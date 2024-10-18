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

    public class ClientIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid client1Id;
        public static Client clientafterupdate;

        public ClientIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllClientsEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/client/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateClient()
        {
            HttpResponseMessage response = await _client.PostAsync("/api/v1/client", new StringContent("{\"Code\":\"CLI123\",\"Name\":\"Client Name\",\"Address\":\"123 Main St\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"ContactPhone\":\"+31 10 123 4567\",\"ContactEmail\":\"test@hr.nl\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var clientid = JsonSerializer.Deserialize<Guid>(responseContent);
            client1Id = clientid;

            Xunit.Assert.NotEqual(Guid.Empty, clientid);
        }

        [Fact, TestPriority(2)]
        public async Task GetAllClients()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/client/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEqual("[]", responseContent);
        }

        [Fact, TestPriority(3)]
        public async Task GetClientById()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/client?id={client1Id}");
            var responseContent = await response.Content.ReadAsStringAsync();

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.NotEqual("{}", responseContent);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateClient()
        {
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/client?guid={client1Id}", new StringContent("{\"Code\":\"CLI123\",\"Name\":\"Client Name Updated\",\"Address\":\"123 Main St\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"ContactPhone\":\"+31 10 123 4567\",\"ContactEmail\":\"test@hr.nl\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));

            HttpResponseMessage responseget = await _client.GetAsync($"/api/v1/client?id={client1Id}");

            var content = await responseget.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            clientafterupdate = JsonSerializer.Deserialize<Client>(content, options);

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal(HttpStatusCode.OK, responseget.StatusCode);
            Xunit.Assert.Equal("Client Name Updated", clientafterupdate.Name);
            Xunit.Assert.Equal(client1Id, clientafterupdate.Id);
        }

        [Fact, TestPriority(5)]
        public async Task GetClientByIdAfterUpdate()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/client?id={clientafterupdate.Id}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var client = JsonSerializer.Deserialize<Supplier>(responseContent, options);

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.NotNull(client);
            Xunit.Assert.Equal(clientafterupdate.Id, client.Id);
            Xunit.Assert.Equal("Client Name Updated", client.Name);
        }

        [Fact, TestPriority(6)]
        public async Task DeleteClient()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/client?guid={clientafterupdate.Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetAllClientsAfterDelete()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/client/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }
    }
}