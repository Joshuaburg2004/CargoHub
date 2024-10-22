using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using CargoHubAlt.Models;
using Newtonsoft.Json;
using Xunit;

namespace MyTests
{
    [TestCaseOrderer("MyTests.PriorityOrderer", "CargoHubTesting")]
    public class TransferIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid TransferId;
        public static Guid ItemId;

        public TransferIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task TestGetAllTransfersEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/transfers/get");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateTrasfer()
        {
            var transfer = new Transfer
            {
                Id = Guid.NewGuid(),
                Reference = "REF123",
                Transfer_from = "Warehouse1",
                Transfer_to = "Warehouse2",
                Transfer_status = "In Progress",
                Created_at = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Updated_at = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Items = new List<Item>
                {
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Uid = "P000000",
                        Code = "PROD123",
                        Description = "Product Description",
                        ShortDescription = "Short Description",
                        UpcCode = "UPC123",
                        ModelNumber = "Model123",
                        CommodityCode = "Commodity123",
                        ItemLine = 1,
                        ItemGroup = 1,
                        ItemType = 1,
                        UnitPurchaseQuantity = 1,
                        UnitOrderQuantity = 1,
                        PackOrderQuantity = 1,
                        SupplierId = 1,
                        SupplierCode = "SUP123",
                        SupplierPartNumber = "SUPPART123",
                        CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                        UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                }
            };
            var jsonContent = JsonConvert.SerializeObject(transfer);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/v1/transfers/add", content);

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            TransferId = transfer.Id;
            ItemId = transfer.Items.First().Id;
        }

        [Fact, TestPriority(2)]
        public async Task TestGetAllTransfers()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/transfers/get");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.NotEqual("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(3)]
        public async Task TestGetTransferById()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/transfers/get/{TransferId}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Transfer? transfer = System.Text.Json.JsonSerializer.Deserialize<Transfer>(content, options);

            Xunit.Assert.NotNull(transfer);
            Xunit.Assert.Equal(TransferId, transfer.Id);
        }

        [Fact, TestPriority(4)]
        public async Task TestGetItemsInTransfer()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/transfers/getitems/{TransferId}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + content);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Item>? items = System.Text.Json.JsonSerializer.Deserialize<List<Item>>(content, options);

            Xunit.Assert.NotNull(items);
            Xunit.Assert.NotEmpty(items);
            Xunit.Assert.Equal(ItemId, items[0].Id);
        }
    }
}