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
        private ShipmentItem _itemAdded = new ShipmentItem(){ ItemId = "P010669", Amount = 16 };
        private Shipment _shipmentToAdd = new Shipment(1, 3, 52, "1973-01-28", "1973-01-30", "1973-02-01", "I", "Pending", "Hoog genot springen afspraak mond bus.", "DHL", "DHL Express", "NextDay", "Automatic", "Ground", 29, 463.0, new List<ShipmentItem> { new ShipmentItem(){ ItemId = "P010669", Amount = 16 } });
        // new StringContent("{\"id\": 3, \"source_id\": 24, \"order_date\": \"1983-09-26T19:06:08Z\", \"request_date\": \"1983-09-30T19:06:08Z\", \"reference\": \"ORD00003\", \"reference_extra\": \"Vergeven kamer goed enkele wiel tussen.\", \"order_status\": \"Delivered\", \"notes\": \"Zeil hoeveel onze map sex ding.\", \"shipping_notes\": \"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\", \"picking_notes\": \"Grof geven politie suiker bodem zuid.\", \"warehouse_id\": 11, \"ship_to\": 0, \"bill_to\": 0, \"shipment_id\": 3, \"total_amount\": 1156.14, \"total_discount\": 420.45, \"total_tax\": 677.42, \"total_surcharge\": 86.03, \"items\": [{\"item_id\": \"P010669\", \"amount\": 16}]}", Encoding.UTF8, "application/json")
        private Order _orderAdded = new Order(3, 24, "1983-09-26T19:06:08Z", "1983-09-30T19:06:08Z", "ORD00003", "Vergeven kamer goed enkele wiel tussen.", "Delivered", "Zeil hoeveel onze map sex ding.", "Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.", "Grof geven politie suiker bodem zuid.", 11, 0, 0, 1, 1156.14, 420.45, 677.42, 86.03, new List<OrderedItem> { new OrderedItem(){ ItemId = "P010669", Amount = 16 } });
        private OrderedItem _itemAddedToOrder = new OrderedItem(){ ItemId = "P010669", Amount = 16 };

        public ShipmentTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
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
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
            Xunit.Assert.Equal(_shipmentToAdd.Id,shipment.Id);
            Xunit.Assert.Equal(_shipmentToAdd.OrderId,shipment.OrderId);
            Xunit.Assert.Equal(_shipmentToAdd.SourceId,shipment.SourceId);
            Xunit.Assert.Equal(_shipmentToAdd.OrderDate,shipment.OrderDate);
            Xunit.Assert.Equal(_shipmentToAdd.RequestDate,shipment.RequestDate);
            Xunit.Assert.Equal(_shipmentToAdd.ShipmentDate,shipment.ShipmentDate);
            Xunit.Assert.Equal(_shipmentToAdd.ShipmentType,shipment.ShipmentType);
            Xunit.Assert.Equal(_shipmentToAdd.ShipmentStatus,shipment.ShipmentStatus);
            Xunit.Assert.Equal(_shipmentToAdd.Notes,shipment.Notes);
            Xunit.Assert.Equal(_shipmentToAdd.CarrierCode,shipment.CarrierCode);
            Xunit.Assert.Equal(_shipmentToAdd.CarrierDescription,shipment.CarrierDescription);
            Xunit.Assert.Equal(_shipmentToAdd.ServiceCode,shipment.ServiceCode);
            Xunit.Assert.Equal(_shipmentToAdd.PaymentType,shipment.PaymentType);
            Xunit.Assert.Equal(_shipmentToAdd.TransferMode,shipment.TransferMode);
            Xunit.Assert.Equal(_shipmentToAdd.TotalPackageCount,shipment.TotalPackageCount);
            Xunit.Assert.Equal(_shipmentToAdd.TotalPackageWeight,shipment.TotalPackageWeight);
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
            Xunit.Assert.Equal(_itemAdded.ItemId,item.ItemId);
            Xunit.Assert.Equal(_itemAdded.Amount,item.Amount);
        }

        [Fact, TestPriority(6)]
        public async Task GetOrdersInShipment()
        {
            // TODO: Finish this tests
            // create order
            var requestUriOrder = "/api/v1/orders";
            await _client.PostAsJsonAsync(requestUriOrder, _orderAdded);

            // do the test
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\":3,", result);
        }

        [Fact, TestPriority(7)]
        public async Task PutShipment()
        {
            var requestUri = "/api/v1/shipments/1";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 1, \"order_id\": 3, \"source_id\": 52, \"order_date\": \"1973-01-28\", \"request_date\": \"1973-01-30\", \"shipment_date\": \"1973-02-01\", \"shipment_type\": \"I\", \"shipment_status\": \"Pending\", \"notes\": \"Hoog genot springen afspraak mond bus.\", \"carrier_code\": \"DHL\", \"carrier_description\": \"DHL Express\", \"service_code\": \"NextDay\", \"payment_type\": \"Automatic\", \"transfer_mode\": \"Ground\", \"total_package_count\": 39, \"total_package_weight\": 463.0, \"items\": [{\"item_id\": \"P010669\", \"amount\": 16}]}", Encoding.UTF8, "application/json"));
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
            Xunit.Assert.Contains("{\"id\":1,\"order_Id\":3,\"source_Id\":52,\"order_Date\":\"1973-01-28\",\"request_Date\":\"1973-01-30\",\"shipment_Date\":\"1973-02-01\",\"shipment_Type\":\"I\",\"shipment_Status\":\"Pending\",\"notes\":\"Hoog genot springen afspraak mond bus.\",\"carrier_Code\":\"DHL\",\"carrier_Description\":\"DHL Express\",\"service_Code\":\"NextDay\",\"payment_Type\":\"Automatic\",\"transfer_Mode\":\"Ground\",\"total_Package_Count\":39,\"total_Package_Weight\":463,\"created_At\":", result);
        }

        [Fact, TestPriority(9)]
        public async Task PutShipmentItems()
        {
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.PutAsync(requestUri, new StringContent("[{\"item_id\": \"P010689\", \"amount\": 16}]", Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Gives Internal Server Error, is because it is updating the Inventories in which the item resides.
            // Because I turned on debug within the python code, the inventories do not exist.
            // Makes sense, but it is not the expected behavior.
        }

        [Fact, TestPriority(10)]
        public async Task GetItemsFromShipmentAfterPutting()
        {
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"item_Id\":\"P010689\",\"amount\":16}", result);
        }

        [Fact, TestPriority(11)]
        public async Task PutOrderInShipment()
        {
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 3, \"source_id\": 52, \"order_date\": \"1983-09-26T19:06:08Z\", \"request_date\": \"1983-09-30T19:06:08Z\", \"reference\": \"ORD00003\", \"reference_extra\": \"Vergeven kamer goed enkele wiel tussen.\", \"order_status\": \"Delivered\", \"notes\": \"Zeil hoeveel onze map sex ding.\", \"shipping_notes\": \"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\", \"picking_notes\": \"Grof geven politie suiker bodem zuid.\", \"warehouse_id\": 11, \"ship_to\": 0, \"bill_to\": 0, \"shipment_id\": 3, \"total_amount\": 1156.14, \"total_discount\": 420.45, \"total_tax\": 677.42, \"total_surcharge\": 86.03, \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\", \"items\": [{\"item_id\": \"P010669\", \"amount\": 16}]}", Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(12)]
        public async Task GetOrdersInShipmentAfterPutting()
        {
            var requestUri = "/api/v1/shipments/1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\":3,\"source_Id\":52,\"order_Date\":\"1983-09-26T19:06:08Z\",\"request_Date\":\"1983-09-30T19:06:08Z\",\"reference\":\"ORD00003\",\"reference_Extra\":\"Vergeven kamer goed enkele wiel tussen.\",\"order_Status\":\"Delivered\",\"notes\":\"Zeil hoeveel onze map sex ding.\",\"shipping_Notes\":\"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\",\"picking_Notes\":\"Grof geven politie suiker bodem zuid.\",\"warehouse_Id\":11,\"ship_To\":0,\"bill_To\":0,\"shipment_Id\":3,\"total_Amount\":1156.14,\"total_Discount\":420.45,\"total_Tax\":677.42,\"total_Surcharge\":86.03,\"created_At\":", result);
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
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
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
    }
}