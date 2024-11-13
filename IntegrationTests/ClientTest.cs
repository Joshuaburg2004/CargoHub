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
    [TestCaseOrderer("IntegrationTests.PriorityOrderer","IntegrationTests")]
    public class ClientTest : BaseTest
    {
        private Client _clientToAdd = new Client(1, "Raymond Inc", "1296 Daniel Road Apt. 349", "Pierceview", "28301", "Colorado", "United States", "Bryan Clark", "242.732.3483x2573", "test@hr.nl");
        private Client _clientToPut = new Client(1, "Raymond Inc JR", "1296 Daniel Road Apt. 349", "Pierceview", "28301", "Colorado", "United States", "Bryan Clark", "242.732.3483x2573", "test@hr.nl");
        public ClientTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
        [Fact,TestPriority(1)]
        public async Task GetAllClients()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Xunit.Assert.Equal("[]",result);
        }
        [Fact,TestPriority(2)]
        public async Task GetOneclientBeforeAdding()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found,but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact,TestPriority(3)]
        public async Task CreateClient()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.PostAsJsonAsync(requestUri,_clientToAdd);
            var result = await response.Content.ReadAsStringAsync();
            Console.Error.WriteLine(result);
            Xunit.Assert.Equal(HttpStatusCode.Created,response.StatusCode);
        }
        [Fact,TestPriority(4)]
        public async Task GetOneClientAfterAdding()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Client? client = await response.Content.ReadFromJsonAsync<Client>();
            Xunit.Assert.NotNull(client);
            Xunit.Assert.Equal(_clientToAdd.Id,client.Id);
            Xunit.Assert.Equal(_clientToAdd.Name,client.Name);
            Xunit.Assert.Equal(_clientToAdd.Address,client.Address);
            Xunit.Assert.Equal(_clientToAdd.City,client.City);
            Xunit.Assert.Equal(_clientToAdd.ZipCode,client.ZipCode);
            Xunit.Assert.Equal(_clientToAdd.Province,client.Province);
            Xunit.Assert.Equal(_clientToAdd.Country,client.Country);
            Xunit.Assert.Equal(_clientToAdd.ContactName,client.ContactName);
            Xunit.Assert.Equal(_clientToAdd.ContactPhone,client.ContactPhone);
            Xunit.Assert.Equal(_clientToAdd.ContactEmail,client.ContactEmail);
        }
        [Fact,TestPriority(5)]
        public async Task PutClient()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.PutAsJsonAsync(requestUri, _clientToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        }
        [Fact,TestPriority(6)]
        public async Task GetOneClientAfterPutting()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Client? client = await response.Content.ReadFromJsonAsync<Client>();
            Xunit.Assert.NotNull(client);
            Xunit.Assert.Equal(_clientToPut.Id,client.Id);
            Xunit.Assert.Equal(_clientToPut.Name,client.Name);
            Xunit.Assert.Equal(_clientToPut.Address,client.Address);
            Xunit.Assert.Equal(_clientToPut.City,client.City);
            Xunit.Assert.Equal(_clientToPut.ZipCode,client.ZipCode);
            Xunit.Assert.Equal(_clientToPut.Province,client.Province);
            Xunit.Assert.Equal(_clientToPut.Country,client.Country);
            Xunit.Assert.Equal(_clientToPut.ContactName,client.ContactName);
            Xunit.Assert.Equal(_clientToPut.ContactPhone,client.ContactPhone);
            Xunit.Assert.Equal(_clientToPut.ContactEmail,client.ContactEmail);
        }
        [Fact,TestPriority(7)]
        public async Task DeleteClient()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        }
        [Fact,TestPriority(8)]
        public async Task GetOneClientAfterDelete()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found,but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact,TestPriority(9)]
        public async Task GetAllClientsAfterDelete()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Xunit.Assert.Equal("[]",result);
        }
    }
}