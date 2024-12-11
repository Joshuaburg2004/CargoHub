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
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class ClientTest : BaseTest
    {
        private Client _clientToAdd = new Client(1, "Raymond Inc", "1296 Daniel Road Apt. 349", "Pierceview", "28301", "Colorado", "United States", "Bryan Clark", "242.732.3483x2573", "test@hr.nl");
        private Client _clientToPut = new Client(1, "Raymond Inc JR", "1296 Daniel Road Apt. 349", "Pierceview", "28301", "Colorado", "United States", "Bryan Clark", "242.732.3483x2573", "test@hr.nl");
        private Client _clientToAddNegative = new Client(-1, "Raymond Inc", "1296 Daniel Road Apt. 349", "Pierceview", "28301", "Colorado", "United States", "Bryan Clark", "242.732.3483x2573", "test@hr.nl");
        private Order _orderToAdd = new Order(1, 1, "2021-10-10", "2021-10-10", "123", "123", "Ordered", "Notes", "ShippingNotes", "PickingNotes", 1, 1, 1, 1, 100.0, 10.0, 5.0, 2.0, null);
        public ClientTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
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
            // It should absolutely be either 400 bad request or 404 not found,but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(3)]
        public async Task CreateClient()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.PostAsJsonAsync(requestUri, _clientToAdd);
            var result = await response.Content.ReadAsStringAsync();
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
            Client? client = await response.Content.ReadFromJsonAsync<Client>();
            Xunit.Assert.NotNull(client);
            Xunit.Assert.Equal(_clientToAdd.Id, client.Id);
            Xunit.Assert.Equal(_clientToAdd.Name, client.Name);
            Xunit.Assert.Equal(_clientToAdd.Address, client.Address);
            Xunit.Assert.Equal(_clientToAdd.City, client.City);
            Xunit.Assert.Equal(_clientToAdd.ZipCode, client.ZipCode);
            Xunit.Assert.Equal(_clientToAdd.Province, client.Province);
            Xunit.Assert.Equal(_clientToAdd.Country, client.Country);
            Xunit.Assert.Equal(_clientToAdd.ContactName, client.ContactName);
            Xunit.Assert.Equal(_clientToAdd.ContactPhone, client.ContactPhone);
            Xunit.Assert.Equal(_clientToAdd.ContactEmail, client.ContactEmail);
        }

        [Fact, TestPriority(5)]
        public async Task GetOrdersByClientNotFound()
        {
            var requestUri = "/api/v1/clients/1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(6)]
        public async Task GetOrderByClient()
        {
            var requestUri = "/api/v1/orders";
            var response = await _client.PostAsJsonAsync(requestUri, _orderToAdd);
            var result = await response.Content.ReadAsStringAsync();

            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var requestUri2 = "/api/v1/clients/1/orders";
            var response2 = await _client.GetAsync(requestUri2);
            var result2 = await response2.Content.ReadAsStringAsync();

            Xunit.Assert.NotNull(result2);
            Xunit.Assert.Equal(HttpStatusCode.OK, response2.StatusCode);

            List<Order>? orders = await response2.Content.ReadFromJsonAsync<List<Order>>();

            Xunit.Assert.NotNull(orders[0]);
            Xunit.Assert.Equal(_orderToAdd.Id, orders[0].Id);
            Xunit.Assert.Equal(_orderToAdd.SourceId, orders[0].SourceId);
            Xunit.Assert.Equal(_orderToAdd.OrderDate, orders[0].OrderDate);
            Xunit.Assert.Equal(_orderToAdd.RequestDate, orders[0].RequestDate);
            Xunit.Assert.Equal(_orderToAdd.Reference, orders[0].Reference);
            Xunit.Assert.Equal(_orderToAdd.ReferenceExtra, orders[0].ReferenceExtra);
            Xunit.Assert.Equal(_orderToAdd.OrderStatus, orders[0].OrderStatus);
            Xunit.Assert.Equal(_orderToAdd.Notes, orders[0].Notes);
            Xunit.Assert.Equal(_orderToAdd.ShippingNotes, orders[0].ShippingNotes);
            Xunit.Assert.Equal(_orderToAdd.PickingNotes, orders[0].PickingNotes);
            Xunit.Assert.Equal(_orderToAdd.WarehouseId, orders[0].WarehouseId);
            Xunit.Assert.Equal(_orderToAdd.ShipTo, orders[0].ShipTo);
            Xunit.Assert.Equal(_orderToAdd.BillTo, orders[0].BillTo);
            Xunit.Assert.Equal(_orderToAdd.ShipmentId, orders[0].ShipmentId);
            Xunit.Assert.Equal(_orderToAdd.TotalAmount, orders[0].TotalAmount);
            Xunit.Assert.Equal(_orderToAdd.TotalDiscount, orders[0].TotalDiscount);
            Xunit.Assert.Equal(_orderToAdd.TotalTax, orders[0].TotalTax);
            Xunit.Assert.Equal(_orderToAdd.TotalSurcharge, orders[0].TotalSurcharge);
        }

        [Fact, TestPriority(7)]
        public async Task PutClient()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.PutAsJsonAsync(requestUri, _clientToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(8)]
        public async Task GetOneClientAfterPutting()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Client? client = await response.Content.ReadFromJsonAsync<Client>();
            Xunit.Assert.NotNull(client);
            Xunit.Assert.Equal(_clientToPut.Id, client.Id);
            Xunit.Assert.Equal(_clientToPut.Name, client.Name);
            Xunit.Assert.Equal(_clientToPut.Address, client.Address);
            Xunit.Assert.Equal(_clientToPut.City, client.City);
            Xunit.Assert.Equal(_clientToPut.ZipCode, client.ZipCode);
            Xunit.Assert.Equal(_clientToPut.Province, client.Province);
            Xunit.Assert.Equal(_clientToPut.Country, client.Country);
            Xunit.Assert.Equal(_clientToPut.ContactName, client.ContactName);
            Xunit.Assert.Equal(_clientToPut.ContactPhone, client.ContactPhone);
            Xunit.Assert.Equal(_clientToPut.ContactEmail, client.ContactEmail);
        }
        [Fact, TestPriority(9)]
        public async Task DeleteClient()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(10)]
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
        [Fact, TestPriority(11)]
        public async Task GetAllClientsAfterDelete()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }

        [Fact, TestPriority(12)]
        public async Task GetClientIdNegative()
        {
            var requestUri = "/api/v1/clients/-1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(13)]
        public async Task AddClientNull()
        {
            var requestUri = "/api/v1/clients";
            var response = await _client.PostAsJsonAsync(requestUri, new { });
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(14)]
        public async Task UpdateClientIdNegative()
        {
            var requestUri = "/api/v1/clients/-1";
            var response = await _client.PutAsJsonAsync(requestUri, _clientToAddNegative);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(15)]
        public async Task UpdateClientNull()
        {
            var requestUri = "/api/v1/clients/1";
            var response = await _client.PutAsJsonAsync(requestUri, new { });
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(16)]
        public async Task DeleteClientIdNegative()
        {
            var requestUri = "/api/v1/clients/-1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(17)]
        public async Task GetOrdersByClientNegative()
        {
            var requestUri = "/api/v1/clients/-1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}