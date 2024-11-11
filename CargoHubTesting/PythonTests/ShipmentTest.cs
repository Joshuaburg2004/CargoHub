using System.Net;
using System.Net.Http;
using PythonTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;
namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class ShipmentTest : BaseTest
    {
        public ShipmentTest() : base() { }
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
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }

        [Fact, TestPriority(3)]
        public async Task CreateShipment()
        {
            var requestUri = "/api/v1/shipments";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"order_id\": 3, \"source_id\": 52, \"order_date\": \"1973-01-28\", \"request_date\": \"1973-01-30\", \"shipment_date\": \"1973-02-01\", \"shipment_type\": \"I\", \"shipment_status\": \"Pending\", \"notes\": \"Hoog genot springen afspraak mond bus.\", \"carrier_code\": \"DHL\", \"carrier_description\": \"DHL Express\", \"service_code\": \"NextDay\", \"payment_type\": \"Automatic\", \"transfer_mode\": \"Ground\", \"total_package_count\": 29, \"total_package_weight\": 463.0, \"created_at\": \"1973-01-28T20:09:11Z\", \"updated_at\": \"1973-01-29T22:09:11Z\", \"items\": [{\"item_id\": \"P010669\", \"amount\": 16}]}", Encoding.UTF8, "application/json"));
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
            Xunit.Assert.Contains("{\"id\":1,\"order_Id\":3,\"source_Id\":52,\"order_Date\":\"1973-01-28\",\"request_Date\":\"1973-01-30\",\"shipment_Date\":\"1973-02-01\",\"shipment_Type\":\"I\",\"shipment_Status\":\"Pending\",\"notes\":\"Hoog genot springen afspraak mond bus.\",\"carrier_Code\":\"DHL\",\"carrier_Description\":\"DHL Express\",\"service_Code\":\"NextDay\",\"payment_Type\":\"Automatic\",\"transfer_Mode\":\"Ground\",\"total_Package_Count\":29,\"total_Package_Weight\":463", result);
        }

        [Fact, TestPriority(5)]
        public async Task GetItemsFromShipmentAfterAdding()
        {
            var requestUri = "/api/v1/shipments/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[{\"item_Id\":\"P010669\",\"amount\":16}]", result);
        }

        [Fact, TestPriority(6)]
        public async Task GetOrdersInShipment()
        {
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
    }
}