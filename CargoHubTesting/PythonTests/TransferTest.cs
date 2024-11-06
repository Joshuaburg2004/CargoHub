using System.Net;

namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class TransferTest : BaseTest
    {
        public TransferTest() : base() { }

        [Fact, TestPriority(1)]
        public async Task GetAllTransfers()
        {
            var requestUri = "/api/v1/transfers";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetOneTransfer()
        {
            var requestUri = "/api/v1/transfers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(3)]
        public async Task GetTransferItems()
        {
            var requestUri = "/api/v1/transfers/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(4)]
        public async Task CreateTransfer()
        {
            var requestUri = "/api/v1/transfers";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 999999, \"reference\": \"TR00001\", \"transfer_from\": null, \"transfer_to\": 9229, \"transfer_status\": \"Completed\", \"created_at\": \"2000-03-11T13:11:14Z\", \"updated_at\": \"2000-03-12T16:11:14Z\", \"items\": [{\"item_id\": \"P007435\", \"amount\": 23}]}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetTransferAfterAdding()
        {
            var requestUri = "/api/v1/transfers/999999";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(6)]
        public async Task PutTransfer()
        {
            var requestUri = "/api/v1/transfers/999999";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 999999, \"reference\": \"TR00001\", \"transfer_from\": null, \"transfer_to\": 1, \"transfer_status\": \"Completed\", \"created_at\": \"2000-03-11T13:11:14Z\", \"updated_at\": \"2000-03-12T16:11:14Z\", \"items\": [{\"item_id\": \"P007435\", \"amount\": 23}]}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetTransferAfterUpdating()
        {
            var requestUri = "/api/v1/transfers/999999";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteTransfer()
        {
            var requestUri = "/api/v1/transfers/999999";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(9)]
        public async Task GetTransferAfterDelete()
        {
            var requestUri = "/api/v1/transfers/999999";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Xunit.Assert.Equal("null", result);
            // No data found, but Status Code is 200 OK => should be 404 Not Found
        }

        [Fact, TestPriority(10)]
        public async Task GetTranserItemsAfterDelete()
        {
            var requestUri = "/api/v1/transfers";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Xunit.Assert.Equal("null", result);
            // No data found, but Status Code is 200 OK => should be 404 Not Found
        }
    }
}