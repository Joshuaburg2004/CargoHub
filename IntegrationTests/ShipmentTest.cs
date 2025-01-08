using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using System.Net.Http.Json;
using CargoHubAlt.Models;
namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class ShipmentTest : BaseTest
    {
        private ShipmentItem _itemAdded = new ShipmentItem() { ItemId = "P010669", Amount = 16 };
        private List<ShipmentItem> _itemPutted = new List<ShipmentItem>() { new ShipmentItem() { ItemId = "P010689", Amount = 16 } };
        private Shipment _shipmentToAdd = new Shipment(1, 3, 52, "1973-01-28", "1973-01-30", "1973-02-01", "I", "Pending", "Hoog genot springen afspraak mond bus.", "DHL", "DHL Express", "NextDay", "Automatic", "Ground", 29, 463.0, new List<ShipmentItem> { new ShipmentItem() { ItemId = "P010669", Amount = 16 } });
        private Shipment _shipmentToPut = new Shipment(1, 3, 52, "1973-01-28", "1973-01-30", "1973-02-01", "I", "Pending", "Hoog genot springen afspraak mond bus.", "DHL", "DHL Express", "NextDay", "Automatic", "Ground", 39, 463.0, new List<ShipmentItem> { new ShipmentItem() { ItemId = "P010669", Amount = 16 } });
        private Order _orderAdded = new Order(3, 24, "1983-09-26T19:06:08Z", "1983-09-30T19:06:08Z", "ORD00003", "Vergeven kamer goed enkele wiel tussen.", "Delivered", "Zeil hoeveel onze map sex ding.", "Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.", "Grof geven politie suiker bodem zuid.", 11, 0, 0, 1, 1156.14, 420.45, 677.42, 86.03, new List<OrderedItem> { new OrderedItem() { ItemId = "P010669", Amount = 16 } });
        private List<int> _orderPutted = new List<int>() { 3 };
        private OrderedItem _itemAddedToOrder = new OrderedItem() { ItemId = "P010669", Amount = 16 };

        public ShipmentTest() { }
        [Fact, TestPriority(1)]
        public async Task GetAllShipments()
        {
            var requestUri = "/api/v1/shipments";

            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }

        [Fact, TestPriority(2)]
        public async Task GetOneShipmentBeforeAdding()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(3)]
        public async Task CreateShipment()
        {
            var requestUri = "/api/v1/shipments";
            var response = await _client.PostAsJsonAsync(requestUri, _shipmentToAdd);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(4)]
        public async Task GetOneShipmentAfterAdding()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Shipment? shipment = await response.Content.ReadFromJsonAsync<Shipment>();
            Xunit.Assert.NotNull(shipment);
            Xunit.Assert.Equal(_shipmentToAdd.Id, shipment.Id);
            Xunit.Assert.Equal(_shipmentToAdd.OrderId, shipment.OrderId);
            Xunit.Assert.Equal(_shipmentToAdd.SourceId, shipment.SourceId);
            Xunit.Assert.Equal(_shipmentToAdd.OrderDate, shipment.OrderDate);
            Xunit.Assert.Equal(_shipmentToAdd.RequestDate, shipment.RequestDate);
            Xunit.Assert.Equal(_shipmentToAdd.ShipmentDate, shipment.ShipmentDate);
            Xunit.Assert.Equal(_shipmentToAdd.ShipmentType, shipment.ShipmentType);
            Xunit.Assert.Equal(_shipmentToAdd.ShipmentStatus, shipment.ShipmentStatus);
            Xunit.Assert.Equal(_shipmentToAdd.Notes, shipment.Notes);
            Xunit.Assert.Equal(_shipmentToAdd.CarrierCode, shipment.CarrierCode);
            Xunit.Assert.Equal(_shipmentToAdd.CarrierDescription, shipment.CarrierDescription);
            Xunit.Assert.Equal(_shipmentToAdd.ServiceCode, shipment.ServiceCode);
            Xunit.Assert.Equal(_shipmentToAdd.PaymentType, shipment.PaymentType);
            Xunit.Assert.Equal(_shipmentToAdd.TransferMode, shipment.TransferMode);
            Xunit.Assert.Equal(_shipmentToAdd.TotalPackageCount, shipment.TotalPackageCount);
            Xunit.Assert.Equal(_shipmentToAdd.TotalPackageWeight, shipment.TotalPackageWeight);
        }

        [Fact, TestPriority(5)]
        public async Task GetItemsFromShipmentAfterAdding()
        {
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<ShipmentItem>? list = await response.Content.ReadFromJsonAsync<List<ShipmentItem>>();
            Xunit.Assert.NotNull(list);
            Assert.Single(list);
            ShipmentItem? item = list.FirstOrDefault();
            Xunit.Assert.NotNull(item);
            Xunit.Assert.Equal(_itemAdded.ItemId, item.ItemId);
            Xunit.Assert.Equal(_itemAdded.Amount, item.Amount);
        }

        [Fact, TestPriority(6)]
        public async Task GetOrdersInShipment()
        {
            // create order
            var requestUriOrder = "/api/v1/orders";
            await _client.PostAsJsonAsync(requestUriOrder, _orderAdded);

            // do the test
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<int>? order = await response.Content.ReadFromJsonAsync<List<int>>();
            Xunit.Assert.NotNull(order);
            Xunit.Assert.Single(order);
            Xunit.Assert.Equal(3, order.FirstOrDefault());
        }

        [Fact, TestPriority(7)]
        public async Task PutShipment()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.PutAsJsonAsync(requestUri, _shipmentToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(8)]
        public async Task GetOneShipmentAfterPutting()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Shipment? shipment = await response.Content.ReadFromJsonAsync<Shipment>();
            Xunit.Assert.NotNull(shipment);
            Xunit.Assert.Equal(_shipmentToPut.Id, shipment.Id);
            Xunit.Assert.Equal(_shipmentToPut.OrderId, shipment.OrderId);
            Xunit.Assert.Equal(_shipmentToPut.SourceId, shipment.SourceId);
            Xunit.Assert.Equal(_shipmentToPut.OrderDate, shipment.OrderDate);
            Xunit.Assert.Equal(_shipmentToPut.RequestDate, shipment.RequestDate);
            Xunit.Assert.Equal(_shipmentToPut.ShipmentDate, shipment.ShipmentDate);
            Xunit.Assert.Equal(_shipmentToPut.ShipmentType, shipment.ShipmentType);
            Xunit.Assert.Equal(_shipmentToPut.ShipmentStatus, shipment.ShipmentStatus);
            Xunit.Assert.Equal(_shipmentToPut.Notes, shipment.Notes);
            Xunit.Assert.Equal(_shipmentToPut.CarrierCode, shipment.CarrierCode);
            Xunit.Assert.Equal(_shipmentToPut.CarrierDescription, shipment.CarrierDescription);
            Xunit.Assert.Equal(_shipmentToPut.ServiceCode, shipment.ServiceCode);
            Xunit.Assert.Equal(_shipmentToPut.PaymentType, shipment.PaymentType);
            Xunit.Assert.Equal(_shipmentToPut.TransferMode, shipment.TransferMode);
            Xunit.Assert.Equal(_shipmentToPut.TotalPackageCount, shipment.TotalPackageCount);
            Xunit.Assert.Equal(_shipmentToPut.TotalPackageWeight, shipment.TotalPackageWeight);
        }

        [Fact, TestPriority(9)]
        public async Task PutShipmentItems()
        {
            var requesterUri = "/api/v1/inventories";
            var responses = await _client.PostAsJsonAsync(requesterUri, new Inventory(10689, "P010689", "Seamless national success", "vzC00315i", new List<int>() { 10054, 18554, 16916, 4855, 23812, 23319, 23080, 317410054, 18554, 16916, 4855, 23812, 23319, 23080, 3174 }, 191, 0, 26, 0, 165));
            var resulter = await responses.Content.ReadAsStringAsync();
            Console.Error.WriteLine(resulter);
            Xunit.Assert.Equal(HttpStatusCode.Created, responses.StatusCode);
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.PutAsJsonAsync(requestUri, _itemPutted);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var requestedUri = "/api/v1/inventories/10689";
            var responsed = await _client.DeleteAsync(requestedUri);
            Xunit.Assert.Equal(HttpStatusCode.OK, responsed.StatusCode);
        }

        [Fact, TestPriority(10)]
        public async Task GetItemsFromShipmentAfterPutting()
        {
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<ShipmentItem>? list = await response.Content.ReadFromJsonAsync<List<ShipmentItem>>();
            Xunit.Assert.NotNull(list);
            Xunit.Assert.Single(list);
            ShipmentItem? item = list.FirstOrDefault();
            Xunit.Assert.NotNull(item);
            Xunit.Assert.Equal(_itemPutted[0].ItemId, item.ItemId);
            Xunit.Assert.Equal(_itemPutted[0].Amount, item.Amount);
        }

        [Fact, TestPriority(11)]
        public async Task PutOrderInShipment()
        {
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.PutAsJsonAsync(requestUri, _orderPutted);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // method returns an int, which means that this method is no longer needed
        [Fact, TestPriority(12)]
        public async Task GetOrdersInShipmentAfterPutting()
        {
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<int>? order = await response.Content.ReadFromJsonAsync<List<int>>();
            Xunit.Assert.NotNull(order);
            Xunit.Assert.Single(order);
            Xunit.Assert.Equal(3, order.FirstOrDefault());
            var requestUriOrder = $"/api/v1/orders/{order.FirstOrDefault()}";
            var responseOrder = await _client.GetAsync(requestUriOrder);
            var resultOrder = await responseOrder.Content.ReadFromJsonAsync<Order>();
            Xunit.Assert.NotNull(resultOrder);
            Xunit.Assert.Equal(_orderAdded.Id, resultOrder.Id);
            Xunit.Assert.Equal("Packed", resultOrder.OrderStatus);
        }

        [Fact, TestPriority(13)]
        public async Task DeleteShipment()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(14)]
        public async Task GetOneShipmentAfterDelete()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }

        [Fact, TestPriority(15)]
        public async Task GetAllShipmentsAfterDelete()
        {
            var requestUri = "/api/v1/shipments";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }

        [Fact, TestPriority(16)]
        public async Task DeleteOrder()
        {
            var requestUri = "/api/v1/orders/3";
            await _client.DeleteAsync(requestUri);
        }

        [Fact, TestPriority(17)]
        public async Task GetShipmentIdNegative()
        {
            var requestUri = "/api/v1/shipments/-1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(18)]
        public async Task GetShipmentItemsIdNegative()
        {
            var requestUri = "/api/v1/shipments/-1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(19)]
        public async Task GetShipmentItemsIdNotFound()
        {
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(20)]
        public async Task GetOrdersInShipmentIdNegative()
        {
            var requestUri = "/api/v1/shipments/-1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(21)]
        public async Task GetOrdersInShipmentIdNotFound()
        {
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}