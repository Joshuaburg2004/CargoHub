using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using CargoHubAlt.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class TransferTest : BaseTest
    {
        public string requestUri = "/api/v2/transfers";
        public static Transfer testtransfer = new Transfer
        {
            Id = 1,
            Reference = "TR00001",
            TransferFrom = 0,
            TransferTo = 9229,
            TransferStatus = "Completed",
            Items = new List<TransferItem>
            {
                new TransferItem { ItemId = "P007435", Amount = 23 }
            }
        };

        public static Transfer testtransfer2 = new Transfer
        {
            Id = 1,
            Reference = "TR00002",
            TransferFrom = 0,
            TransferTo = 1,
            TransferStatus = "Completed",
            Items = new List<TransferItem>
            {
                new TransferItem { ItemId = "P007435", Amount = 23 }
            }
        };

        public TransferTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact, TestPriority(1)]
        public async Task GetAllTransfersEmpty()
        {
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetOneTransfer()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(3)]
        public async Task GetTransferItems()
        {
            var response = await _client.GetAsync($"{requestUri}/1/items");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(4)]
        public async Task CreateTransfer()
        {
            var response = await _client.PostAsJsonAsync(requestUri, testtransfer);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetTransferAfterAdding()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();

            Transfer? res = await response.Content.ReadFromJsonAsync<Transfer>();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.NotNull(res);

            Xunit.Assert.Equal(testtransfer.Id, res.Id);
            Xunit.Assert.Equal(testtransfer.Reference, res.Reference);
            Xunit.Assert.Equal(testtransfer.TransferFrom, res.TransferFrom);
            Xunit.Assert.Equal(testtransfer.TransferTo, res.TransferTo);
            Xunit.Assert.Equal(testtransfer.TransferStatus, res.TransferStatus);
            Xunit.Assert.Equal(testtransfer.CreatedAt, res.CreatedAt);
            Xunit.Assert.Equal(testtransfer.UpdatedAt, res.UpdatedAt);
        }

        [Fact, TestPriority(6)]
        public async Task GetItemsInTranfser()
        {
            var response = await _client.GetAsync($"{requestUri}/1/items");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            List<TransferItem>? res = await response.Content.ReadFromJsonAsync<List<TransferItem>>();
            Xunit.Assert.NotNull(res);

            Xunit.Assert.Equal(testtransfer.Items[0].ItemId, res[0].ItemId);
            Xunit.Assert.Equal(testtransfer.Items[0].Amount, res[0].Amount);
        }

        [Fact, TestPriority(7)]
        public async Task GetAllTransfers()
        {
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            List<Transfer>? res = await response.Content.ReadFromJsonAsync<List<Transfer>>();
            Xunit.Assert.NotNull(res);

            Xunit.Assert.Equal(testtransfer.Id, res[0].Id);
            Xunit.Assert.Equal(testtransfer.Reference, res[0].Reference);
            Xunit.Assert.Equal(testtransfer.TransferFrom, res[0].TransferFrom);
            Xunit.Assert.Equal(testtransfer.TransferTo, res[0].TransferTo);
            Xunit.Assert.Equal(testtransfer.TransferStatus, res[0].TransferStatus);
        }


        [Fact, TestPriority(8)]
        public async Task PutTransfer()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/1", testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(9)]
        public async Task CommitTransfer()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/1/commit", testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(10)]
        public async Task GetTransferAfterUpdating()
        {
            var response = await _client.DeleteAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();

            Transfer? res = await response.Content.ReadFromJsonAsync<Transfer>();

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(11)]
        public async Task GetTransferAfterDelete()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(12)]
        public async Task GetTranserItemsAfterDelete()
        {
            var response = await _client.GetAsync($"{requestUri}/1/items");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(13)]
        public async Task GetTransferNegative()
        {
            var response = await _client.GetAsync($"{requestUri}/-1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(14)]
        public async Task GetItemsTransferNegative()
        {
            var response = await _client.GetAsync($"{requestUri}/-1/items");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(15)]
        public async Task PutTransferNegative()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/-1", testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(16)]
        public async Task DeleteTransferNegative()
        {
            var response = await _client.DeleteAsync($"{requestUri}/-1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(17)]
        public async Task DeleteTransferNotFound()
        {
            var response = await _client.DeleteAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(18)]
        public async Task UpdateTransferNotFound()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/1", testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(19)]
        public async Task CommitTransferIdNegative()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/-1/commit", testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(20)]
        public async Task CommitTransferNotFound()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/1/commit", testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}