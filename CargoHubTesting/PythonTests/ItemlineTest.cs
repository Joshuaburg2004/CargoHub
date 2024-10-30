using System.Net;
using System.Net.Http;
using PythonTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class ItemlineTest : BaseTest
    {
        public ItemlineTest() : base() { }
        [Fact, TestPriority(1)]
        public async Task GetAllItemlines()
        {
            var requestUri = "/api/v1/item_lines";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneItemlineBeforeAdding()
        {
            var requestUri = "/api/v1/item_lines/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(3)]
        public async Task CreateItemline()
        {
            var requestUri = "/api/v1/item_lines";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"name\": \"gadget\", \"description\": \"gadget description\", \"created_at\": \"2022-08-18 07:05:25\", \"updated_at\": \"2023-05-15 15:44:28\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(result, "{\"id\": 1, \"name\": \"gadget\", \"description\": \"gadget description\", \"created_at\": \"2022-08-18 07:05:25\", \"updated_at\": \"2023-05-15 15:44:28\"}");
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneItemlineAfterAdding()
        {
            var requestUri = "/api/v1/item_lines/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"name\": \"gadget\", \"description\": \"gadget description\"}", result);
        }
        [Fact, TestPriority(5)]
        public async Task PutItemline()
        {
            var requestUri = "/api/v1/item_lines/1";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 1, \"name\": \"gadgets\", \"description\": \"gadget description\", \"created_at\": \"2022-08-18 07:05:25\", \"updated_at\": \"2023-05-15 15:44:28\"}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(6)]
        public async Task GetOneItemlineAfterPutting()
        {
            var requestUri = "/api/v1/item_lines/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"name\": \"gadgets\", \"description\": \"gadget description\"}", result);
        }
        [Fact, TestPriority(7)]
        public async Task DeleteItemline()
        {
            var requestUri = "/api/v1/item_lines/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(8)]
        public async Task GetOneItemlineAfterDelete()
        {
            var requestUri = "/api/v1/item_lines/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(9)]
        public async Task GetAllItemlinesAfterDelete()
        {
            var requestUri = "/api/v1/item_lines";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
    }
}