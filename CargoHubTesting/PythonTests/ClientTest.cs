using System.Net;
using System.Net.Http;
using PythonTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class ClientTest : BaseTest
    {
        public ClientTest() : base() { }
        [Fact, TestPriority(1)]
        public async Task GetAllClients()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneclientBeforeAdding()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(3)]
        public async Task CreateClient()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"name\": \"Raymond Inc\", \"address\": \"1296 Daniel Road Apt. 349\", \"city\": \"Pierceview\", \"zip_code\": \"28301\", \"province\": \"Colorado\", \"country\": \"United States\", \"contact_name\": \"Bryan Clark\", \"contact_phone\": \"242.732.3483x2573\", \"contact_email\": \"test@hr.nl\", \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Console.Error.WriteLine(result);
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneClientAfterAdding()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"name\": \"Raymond Inc\", \"address\": \"1296 Daniel Road Apt. 349\", \"city\": \"Pierceview\", \"zip_code\": \"28301\", \"province\": \"Colorado\", \"country\": \"United States\", \"contact_name\": \"Bryan Clark\", \"contact_phone\": \"242.732.3483x2573\", \"contact_email\": \"test@hr.nl\"", result);
        }
        [Fact, TestPriority(5)]
        public async Task PutClient()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 1, \"name\": \"Raymond Inc JR\", \"address\": \"1296 Daniel Road Apt. 349\", \"city\": \"Pierceview\", \"zip_code\": \"28301\", \"province\": \"Colorado\", \"country\": \"United States\", \"contact_name\": \"Bryan Clark\", \"contact_phone\": \"242.732.3483x2573\", \"contact_email\": \"test@hr.nl\", \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(6)]
        public async Task GetOneClientAfterPutting()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"name\": \"Raymond Inc JR\", \"address\": \"1296 Daniel Road Apt. 349\", \"city\": \"Pierceview\", \"zip_code\": \"28301\", \"province\": \"Colorado\", \"country\": \"United States\", \"contact_name\": \"Bryan Clark\", \"contact_phone\": \"242.732.3483x2573\", \"contact_email\": \"", result);
        }
        [Fact, TestPriority(7)]
        public async Task DeleteClient()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(8)]
        public async Task GetOneClientAfterDelete()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(9)]
        public async Task GetAllClientsAfterDelete()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
    }
}