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

    public class Item_typeIntegratieTest : BaseTest
    {

        private ItemType _testType = new(1, "Laptop", "");
        private ItemType _putType = new(1, "smart name", "smart description");
        private Item _testItem = new("P000004", "sjQ23408K", "Face-to-face clear-thinking complexity",
         "must", "6523540947122", "63-OFFTq0T", "oTo304", 1, 1, 1, 1, 1, 1, 1, "SUP423", "E-86805-uTM");

        private static string _requestUri = "/api/v1/item_types";

        public Item_typeIntegratieTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        { }

        [Fact, TestPriority(0)]
        public async Task CreateItemType()
        {
            var requestUri = "/api/v1/item_types";
            var response = await _client.PostAsJsonAsync(requestUri, _testType);
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task CreateItem()
        {
            var requestUri = "/api/v1/items";
            var response = await _client.PostAsJsonAsync(requestUri, _testItem);
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(26)]
        public async Task GetAllItemTypesOne()
        {
            HttpResponseMessage response = await _client.GetAsync(_requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            ItemType[]? itemTypes = await response.Content.ReadFromJsonAsync<ItemType[]>();
            Xunit.Assert.Equal(_testType.Id, itemTypes[0].Id);
            Xunit.Assert.Equal(_testType.Name, itemTypes[0].Name);
            Xunit.Assert.Equal(_testType.Description, itemTypes[0].Description);
        }

        [Fact, TestPriority(27)]
        public async Task GetItemType()
        {
            var requestUri = "/api/v1/item_types/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            ItemType? itemType = await response.Content.ReadFromJsonAsync<ItemType>();
            Xunit.Assert.Equal(_testType.Id, itemType.Id);
            Xunit.Assert.Equal(_testType.Name, itemType.Name);
            Xunit.Assert.Equal(_testType.Description, itemType.Description);
        }

        [Fact, TestPriority(28)]
        public async Task UpdateItemtype()
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{_requestUri}/1", _putType);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(29)]
        public async Task GetUpdatedItemType()
        {
            HttpResponseMessage response = await _client.GetAsync($"{_requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            ItemType? itemType = await response.Content.ReadFromJsonAsync<ItemType>();
            Xunit.Assert.Equal(_putType.Id, itemType.Id);
            Xunit.Assert.Equal(_putType.Name, itemType.Name);
            Xunit.Assert.Equal(_putType.Description, itemType.Description);
        }

        [Fact, TestPriority(30)]
        public async Task GetItemTypeItems()
        {
            var requestUri = "/api/v1/item_types/1/items";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Item[]? items = await response.Content.ReadFromJsonAsync<Item[]>();
            Xunit.Assert.Equal(_testItem.Uid, items[0].Uid);
            Xunit.Assert.Equal(_testItem.Code, items[0].Code);
            Xunit.Assert.Equal(_testItem.Description, items[0].Description);
            Xunit.Assert.Equal(_testItem.ShortDescription, items[0].ShortDescription);
            Xunit.Assert.Equal(_testItem.UpcCode, items[0].UpcCode);
            Xunit.Assert.Equal(_testItem.ModelNumber, items[0].ModelNumber);
            Xunit.Assert.Equal(_testItem.CommodityCode, items[0].CommodityCode);
            Xunit.Assert.Equal(_testItem.ItemLine, items[0].ItemLine);
            Xunit.Assert.Equal(_testItem.ItemGroup, items[0].ItemGroup);
            Xunit.Assert.Equal(_testItem.ItemType, items[0].ItemType);
            Xunit.Assert.Equal(_testItem.UnitPurchaseQuantity, items[0].UnitPurchaseQuantity);
            Xunit.Assert.Equal(_testItem.UnitOrderQuantity, items[0].UnitOrderQuantity);
            Xunit.Assert.Equal(_testItem.PackOrderQuantity, items[0].PackOrderQuantity);
            Xunit.Assert.Equal(_testItem.SupplierId, items[0].SupplierId);
            Xunit.Assert.Equal(_testItem.SupplierCode, items[0].SupplierCode);
            Xunit.Assert.Equal(_testItem.SupplierPartNumber, items[0].SupplierPartNumber);
        }

        [Fact, TestPriority(31)]
        public async Task GetWrongItemtype()
        {
            HttpResponseMessage response = await _client.GetAsync($"{_requestUri}/2");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(32)]
        public async Task DeleteItemtype()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_requestUri}/1");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("", responseContent);
        }

        [Fact, TestPriority(33)]
        public async Task GetItemTypeAfterDelete()
        {
            HttpResponseMessage response = await _client.GetAsync(_requestUri);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        [Fact, TestPriority(34)]
        public async Task DeleteItem()
        {
            HttpResponseMessage response = await _client.DeleteAsync("/api/v1/items/P000004");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}