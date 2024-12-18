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
    public class OrderTest : BaseTest
    {
        private Order neworder = new Order
        {
            Id = 1,
            SourceId = 52,
            OrderDate = "1983-09-26T19:06:08Z",
            RequestDate = "1983-09-30T19:06:08Z",
            Reference = "ORD00003",
            ReferenceExtra = "Vergeven kamer goed enkele wiel tussen.",
            OrderStatus = "Delivered",
            Notes = "Zeil hoeveel onze map",
            ShippingNotes = "Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.",
            PickingNotes = "Grof geven politie suiker bodem zuid.",
            WarehouseId = 11,
            ShipTo = 1,
            BillTo = 1,
            ShipmentId = 3,
            TotalAmount = 1156.14,
            TotalDiscount = 420.45,
            TotalTax = 677.42,
            TotalSurcharge = 86.03,
            Items = new System.Collections.Generic.List<OrderedItem> { new OrderedItem { ItemId = "P010689", Amount = 16 } }
        };

        Order updatedorder = new Order
        {
            Id = 1,
            SourceId = 52,
            OrderDate = "1983-09-26T19:06:08Z",
            RequestDate = "1983-09-30T19:06:08Z",
            Reference = "ORD00003",
            ReferenceExtra = "Vergeven kamer goed enkele wiel tussen.",
            OrderStatus = "Pending",
            Notes = "Zeil hoeveel onze map",
            ShippingNotes = "Ontvangen schoon voorzichtig instrument ster vijver kunnen raam.",
            PickingNotes = "Grof geven politie suiker bodem zuid.",
            WarehouseId = 11,
            ShipTo = 1,
            BillTo = 1,
            ShipmentId = 3,
            TotalAmount = 1156.14,
            TotalDiscount = 420.45,
            TotalTax = 677.42,
            TotalSurcharge = 86.03,
            Items = new System.Collections.Generic.List<OrderedItem> { new OrderedItem { ItemId = "P010689", Amount = 16 } }
        };

        public OrderTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
        [Fact, TestPriority(1)]
        public async Task GetAllOrders()
        {
            var requestUri = "/api/v1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
            var response = await _client.PostAsJsonAsync(requestUri, neworder);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(4)]
        public async Task GetAllOrderAfterOneAdd()
        {
            var requestUri = "/api/v1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            List<Order> orders = await response.Content.ReadFromJsonAsync<List<Order>>();

            Xunit.Assert.NotNull(orders);
            Xunit.Assert.Equal(neworder.Id, orders[0].Id);
            Xunit.Assert.Equal(neworder.SourceId, orders[0].SourceId);
            Xunit.Assert.Equal(neworder.OrderDate, orders[0].OrderDate);
            Xunit.Assert.Equal(neworder.RequestDate, orders[0].RequestDate);
            Xunit.Assert.Equal(neworder.Reference, orders[0].Reference);
            Xunit.Assert.Equal(neworder.ReferenceExtra, orders[0].ReferenceExtra);
            Xunit.Assert.Equal(neworder.OrderStatus, orders[0].OrderStatus);
            Xunit.Assert.Equal(neworder.Notes, orders[0].Notes);
            Xunit.Assert.Equal(neworder.ShippingNotes, orders[0].ShippingNotes);
            Xunit.Assert.Equal(neworder.PickingNotes, orders[0].PickingNotes);
            Xunit.Assert.Equal(neworder.WarehouseId, orders[0].WarehouseId);
            Xunit.Assert.Equal(neworder.ShipTo, orders[0].ShipTo);
            Xunit.Assert.Equal(neworder.BillTo, orders[0].BillTo);
            Xunit.Assert.Equal(neworder.ShipmentId, orders[0].ShipmentId);
            Xunit.Assert.Equal(neworder.TotalAmount, orders[0].TotalAmount);
            Xunit.Assert.Equal(neworder.TotalDiscount, orders[0].TotalDiscount);
            Xunit.Assert.Equal(neworder.TotalTax, orders[0].TotalTax);
            Xunit.Assert.Equal(neworder.TotalSurcharge, orders[0].TotalSurcharge);
        }


        [Fact, TestPriority(5)]
        public async Task GetOneOrderAfterAdding()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Order order = await response.Content.ReadFromJsonAsync<Order>();

            Xunit.Assert.NotNull(order);
            Xunit.Assert.Equal(neworder.Id, order.Id);
            Xunit.Assert.Equal(neworder.SourceId, order.SourceId);
            Xunit.Assert.Equal(neworder.OrderDate, order.OrderDate);
            Xunit.Assert.Equal(neworder.RequestDate, order.RequestDate);
            Xunit.Assert.Equal(neworder.Reference, order.Reference);
            Xunit.Assert.Equal(neworder.ReferenceExtra, order.ReferenceExtra);
            Xunit.Assert.Equal(neworder.OrderStatus, order.OrderStatus);
            Xunit.Assert.Equal(neworder.Notes, order.Notes);
            Xunit.Assert.Equal(neworder.ShippingNotes, order.ShippingNotes);
            Xunit.Assert.Equal(neworder.PickingNotes, order.PickingNotes);
            Xunit.Assert.Equal(neworder.WarehouseId, order.WarehouseId);
            Xunit.Assert.Equal(neworder.ShipTo, order.ShipTo);
            Xunit.Assert.Equal(neworder.BillTo, order.BillTo);
            Xunit.Assert.Equal(neworder.ShipmentId, order.ShipmentId);
            Xunit.Assert.Equal(neworder.TotalAmount, order.TotalAmount);
            Xunit.Assert.Equal(neworder.TotalDiscount, order.TotalDiscount);
            Xunit.Assert.Equal(neworder.TotalTax, order.TotalTax);
            Xunit.Assert.Equal(neworder.TotalSurcharge, order.TotalSurcharge);
        }
        [Fact, TestPriority(6)]
        public async Task GetItemsFromOrderAfterAdding()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Xunit.Assert.Contains("[{\"itemId\":\"P010689\",\"amount\":16}]", result);

        }
        [Fact, TestPriority(7)]
        public async Task GetPendingOrdersEmpty()
        {
            var requestUri = "/api/v2/orders/pending";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Xunit.Assert.Equal("No undelivered Orders found", result);

        }

        [Fact, TestPriority(8)]
        public async Task PutOrder()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.PutAsJsonAsync(requestUri, updatedorder);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(9)]
        public async Task GetOneOrderAfterPutting()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Order order = await response.Content.ReadFromJsonAsync<Order>();

            Xunit.Assert.NotNull(order);
            Xunit.Assert.Equal(updatedorder.Id, order.Id);
            Xunit.Assert.Equal(updatedorder.SourceId, order.SourceId);
            Xunit.Assert.Equal(updatedorder.OrderDate, order.OrderDate);
            Xunit.Assert.Equal(updatedorder.RequestDate, order.RequestDate);
            Xunit.Assert.Equal(updatedorder.Reference, order.Reference);
            Xunit.Assert.Equal(updatedorder.ReferenceExtra, order.ReferenceExtra);
            Xunit.Assert.Equal(updatedorder.OrderStatus, order.OrderStatus);
            Xunit.Assert.Equal(updatedorder.Notes, order.Notes);
            Xunit.Assert.Equal(updatedorder.ShippingNotes, order.ShippingNotes);
            Xunit.Assert.Equal(updatedorder.PickingNotes, order.PickingNotes);
            Xunit.Assert.Equal(updatedorder.WarehouseId, order.WarehouseId);
            Xunit.Assert.Equal(updatedorder.ShipTo, order.ShipTo);
            Xunit.Assert.Equal(updatedorder.BillTo, order.BillTo);
            Xunit.Assert.Equal(updatedorder.ShipmentId, order.ShipmentId);
            Xunit.Assert.Equal(updatedorder.TotalAmount, order.TotalAmount);
            Xunit.Assert.Equal(updatedorder.TotalDiscount, order.TotalDiscount);
            Xunit.Assert.Equal(updatedorder.TotalTax, order.TotalTax);
            Xunit.Assert.Equal(updatedorder.TotalSurcharge, order.TotalSurcharge);
        }
        [Fact, TestPriority(10)]
        public async Task GetUndeliveredOrdersOne()
        {
            var requestUri = "/api/v2/orders/pending";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Order>>();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Single(result);

            var order = result.FirstOrDefault();
            Xunit.Assert.NotNull(order);
            Xunit.Assert.Equal(updatedorder.Id, order.Id);
            Xunit.Assert.Equal(updatedorder.SourceId, order.SourceId);
            Xunit.Assert.Equal(updatedorder.OrderDate, order.OrderDate);
            Xunit.Assert.Equal(updatedorder.RequestDate, order.RequestDate);
            Xunit.Assert.Equal(updatedorder.Reference, order.Reference);
            Xunit.Assert.Equal(updatedorder.ReferenceExtra, order.ReferenceExtra);
            Xunit.Assert.Equal(updatedorder.OrderStatus, order.OrderStatus);
            Xunit.Assert.Equal(updatedorder.Notes, order.Notes);
            Xunit.Assert.Equal(updatedorder.ShippingNotes, order.ShippingNotes);
            Xunit.Assert.Equal(updatedorder.PickingNotes, order.PickingNotes);
            Xunit.Assert.Equal(updatedorder.WarehouseId, order.WarehouseId);
            Xunit.Assert.Equal(updatedorder.ShipTo, order.ShipTo);
            Xunit.Assert.Equal(updatedorder.BillTo, order.BillTo);
            Xunit.Assert.Equal(updatedorder.ShipmentId, order.ShipmentId);
            Xunit.Assert.Equal(updatedorder.TotalAmount, order.TotalAmount);
            Xunit.Assert.Equal(updatedorder.TotalDiscount, order.TotalDiscount);
            Xunit.Assert.Equal(updatedorder.TotalTax, order.TotalTax);
            Xunit.Assert.Equal(updatedorder.TotalSurcharge, order.TotalSurcharge);
        }


        [Fact, TestPriority(11)]
        public async Task PutOrderItems()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.PutAsJsonAsync(requestUri, new System.Collections.Generic.List<OrderedItem> { new OrderedItem { ItemId = "P010689", Amount = 20 } });
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Gives Internal Server Error, is because it is updating the Inventories in which the item resides.
            // Because I turned on debug within the python code, the inventories do not exist.
            // Makes sense, but it is not the expected behavior.
        }
        [Fact, TestPriority(12)]
        public async Task GetItemsFromOrderAfterPutting()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Contains("[{\"itemId\":\"P010689\",\"amount\":20}]", result);
        }
        [Fact, TestPriority(13)]
        public async Task DeleteOrder()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(14)]
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
        [Fact, TestPriority(15)]
        public async Task GetAllOrdersAfterDelete()
        {
            var requestUri = "/api/v1/orders";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(16)]
        public async Task GetOrderIdNegative()
        {
            var requestUri = "/api/v1/orders/-1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(17)]
        public async Task GetOrderItemsIdNegative()
        {
            var requestUri = "/api/v1/orders/-1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(18)]
        public async Task GetorderItemsNotFound()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(19)]
        public async Task PutOrderNotFound()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.PutAsJsonAsync(requestUri, updatedorder);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(20)]
        public async Task PutOrderItemsNotFound()
        {
            var requestUri = "/api/v1/orders/1/items";
            var response = await _client.PutAsJsonAsync(requestUri, new System.Collections.Generic.List<OrderedItem> { new OrderedItem { ItemId = "P010689", Amount = 20 } });
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(21)]
        public async Task DeleteOrderNotFound()
        {
            var requestUri = "/api/v1/orders/1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(22)]
        public async Task UpdateOrderIdNegative()
        {
            var requestUri = "/api/v1/orders/-1";
            var response = await _client.PutAsJsonAsync(requestUri, updatedorder);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(23)]
        public async Task UpdateOrderItemsIdNegative()
        {
            var requestUri = "/api/v1/orders/-1/items";
            var response = await _client.PutAsJsonAsync(requestUri, new System.Collections.Generic.List<OrderedItem> { new OrderedItem { ItemId = "P010689", Amount = 20 } });
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(24)]
        public async Task DeleteOrderIdNegative()
        {
            var requestUri = "/api/v1/orders/-1";
            var response = await _client.DeleteAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}