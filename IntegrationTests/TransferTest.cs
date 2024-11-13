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
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(3)]
        public async Task GetTransferItems()
        {
            var requestUri = "/api/v1/transfers/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(4)]
        public async Task CreateTransfer()
        {
            var requestUri = "/api/v1/transfers";
            var response = await _client.PostAsJsonAsync(requestUri, testtransfer);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetTransferAfterAdding()
        {
            var requestUri = "/api/v1/transfers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            Transfer res = await response.Content.ReadFromJsonAsync<Transfer>();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Xunit.Assert.Equal(testtransfer.Id, res.Id);
            Xunit.Assert.Equal(testtransfer.Reference, res.Reference);
            Xunit.Assert.Equal(testtransfer.TransferFrom, res.TransferFrom);
            Xunit.Assert.Equal(testtransfer.TransferTo, res.TransferTo);
            Xunit.Assert.Equal(testtransfer.TransferStatus, res.TransferStatus);
            Xunit.Assert.Equal(testtransfer.CreatedAt, res.CreatedAt);
            Xunit.Assert.Equal(testtransfer.UpdatedAt, res.UpdatedAt);
        }

        [Fact, TestPriority(6)]
        public async Task PutTransfer()
        {
            var requestUri = "/api/v1/transfers/1";
            var response = await _client.PutAsJsonAsync(requestUri, testtransfer2);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetTransferAfterUpdating()
        {
            var requestUri = "/api/v1/transfers/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            Transfer res = await response.Content.ReadFromJsonAsync<Transfer>();
           
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Xunit.Assert.Equal(testtransfer2.Id, res.Id);
            Xunit.Assert.Equal(testtransfer2.Reference, res.Reference);
            Xunit.Assert.Equal(testtransfer2.TransferFrom, res.TransferFrom);
            Xunit.Assert.Equal(testtransfer2.TransferTo, res.TransferTo);
            Xunit.Assert.Equal(testtransfer2.TransferStatus, res.TransferStatus);
            Xunit.Assert.Equal(testtransfer2.CreatedAt, res.CreatedAt);
            Xunit.Assert.Equal(testtransfer2.UpdatedAt, res.UpdatedAt);
        }

        [Fact, TestPriority(8)]
        public async Task GetTransferAfterDelete()
        {
            var requestUri = "/api/v1/transfers/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(9)]
        public async Task GetTranserItemsAfterDelete()
        {
            var requestUri = "/api/v1/transfers/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }
    }
}