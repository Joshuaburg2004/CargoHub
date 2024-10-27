using System.Net;
using System.Net.Http;
using PythonTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class OrderTest : BaseTest
    {
        public OrderTest() : base() { }
        [Fact, TestPriority(1)]
        public async Task GetAllOrders()
        {
            var requestUri = "/api/v1/orders";

            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneOrderBeforeAdding()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(3)]
        public async Task CreateOrder()
        {
            var requestUri = "/api/v1/orders";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 1, \"source_id\": 52, \"order_date\": \"1983-09-26T19:06:08Z\", \"request_date\": \"1983-09-30T19:06:08Z\", \"reference\": \"ORD00003\", \"reference_extra\": \"Vergeven kamer goed enkele wiel tussen.\", \"order_status\": \"Delivered\", \"notes\": \"Zeil hoeveel onze map sex ding.\", \"shipping_notes\": \"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\", \"picking_notes\": \"Grof geven politie suiker bodem zuid.\", \"warehouse_id\": 11, \"ship_to\": null, \"bill_to\": null, \"shipment_id\": 3, \"total_amount\": 1156.14, \"total_discount\": 420.45, \"total_tax\": 677.42, \"total_surcharge\": 86.03, \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\", \"items\": [{\"item_id\": \"P010669\", \"amount\": 16}]}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneOrderAfterAdding()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"source_id\": 52, \"order_date\": \"1983-09-26T19:06:08Z\", \"request_date\": \"1983-09-30T19:06:08Z\", \"reference\": \"ORD00003\", \"reference_extra\": \"Vergeven kamer goed enkele wiel tussen.\", \"order_status\": \"Delivered\", \"notes\": \"Zeil hoeveel onze map sex ding.\", \"shipping_notes\": \"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\", \"picking_notes\": \"Grof geven politie suiker bodem zuid.\", \"warehouse_id\": 11, \"ship_to\": null, \"bill_to\": null, \"shipment_id\": 3, \"total_amount\": 1156.14, \"total_discount\": 420.45, \"total_tax\": 677.42, \"total_surcharge\": 86.03", result);
        }
        [Fact, TestPriority(5)]
        public async Task GetItemsFromOrderAfterAdding()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[{\"item_id\": \"P010669\", \"amount\": 16}]", result);
        }
        [Fact, TestPriority(6)]
        public async Task PutOrder()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 1, \"source_id\": 52, \"order_date\": \"1983-09-26T19:06:08Z\", \"request_date\": \"1983-09-30T19:06:08Z\", \"reference\": \"ORD00003\", \"reference_extra\": \"Vergeven kamer goed enkele wiel tussen.\", \"order_status\": \"Delivered\", \"notes\": \"Zeil hoeveel onze map sex ding.\", \"shipping_notes\": \"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\", \"picking_notes\": \"Grof geven politie suiker bodem zuid.\", \"warehouse_id\": 11, \"ship_to\": null, \"bill_to\": null, \"shipment_id\": 3, \"total_amount\": 1156.14, \"total_discount\": 420.45, \"total_tax\": 677.42, \"total_surcharge\": 86.03, \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\", \"items\": [{\"item_id\": \"P010669\", \"amount\": 16}]}"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(7)]
        public async Task GetOneOrderAfterPutting()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"id\": 1, \"source_id\": 52, \"order_date\": \"1983-09-26T19:06:08Z\", \"request_date\": \"1983-09-30T19:06:08Z\", \"reference\": \"ORD00003\", \"reference_extra\": \"Vergeven kamer goed enkele wiel tussen.\", \"order_status\": \"Delivered\", \"notes\": \"Zeil hoeveel onze map sex ding.\", \"shipping_notes\": \"Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.\", \"picking_notes\": \"Grof geven politie suiker bodem zuid.\", \"warehouse_id\": 11, \"ship_to\": null, \"bill_to\": null, \"shipment_id\": 3, \"total_amount\": 1156.14, \"total_discount\": 420.45, \"total_tax\": 677.42, \"total_surcharge\": 86.03, \"created_at\": ", result);
        }

        [Fact, TestPriority(8)]
        public async Task PutOrderItems()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.PutAsync(requestUri, new StringContent("[{\"item_id\": \"P010689\", \"amount\": 16}]"));
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Gives Internal Server Error, is because it is updating the Inventories in which the item resides.
            // Because I turned on debug within the python code, the inventories do not exist.
            // Makes sense, but it is not the expected behavior.
        }
        [Fact, TestPriority(9)]
        public async Task GetItemsFromOrderAfterPutting()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("{\"item_id\": \"P010689\", \"amount\": 16}", result);
        }
        [Fact, TestPriority(10)]
        public async Task DeleteOrder()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(11)]
        public async Task GetOneOrderAfterDelete()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(12)]
        public async Task GetAllOrdersAfterDelete()
        {
            var requestUri = "/api/v1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
    }
}