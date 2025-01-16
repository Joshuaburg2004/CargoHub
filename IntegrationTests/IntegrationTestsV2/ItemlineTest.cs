using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using CargoHubAlt.Models;
using System.Net.Http.Json;

namespace IntegrationTests;

[TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]

public class ItemLineTest : BaseTest
{
    public string requestUri = "/api/v2/item_lines";
    public string requestUriItem = "/api/v2/items";
    private ItemLine _itemLineCreate = new ItemLine(1, "Laptop", "Never gonna give you up");
    private ItemLine _itemLinePut = new ItemLine(1, "Laptop", "Never gonna let you down");
    private Item _item = new Item("P000005", "Laptop", "Never gonna give you up", "Never gonna", "123456789", "123456789", "123456789", 1, 1, 1, 1, 1, 1, 1, "123456789", "123456789");

    public ItemLineTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact, TestPriority(0)]
    public async Task CreateItemLine()
    {
        var response = await _client.PostAsJsonAsync(requestUri, _itemLineCreate);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact, TestPriority(1)]
    public async Task CreateItem()
    {
        var response = await _client.PostAsJsonAsync(requestUriItem, _item);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact, TestPriority(2)]
    public async Task GetAllItemLinesOne()
    {
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemLine[]? itemLines = await response.Content.ReadFromJsonAsync<ItemLine[]>();
        Xunit.Assert.NotNull(itemLines);
        Xunit.Assert.Equal(_itemLineCreate.Id, itemLines[0].Id);
        Xunit.Assert.Equal(_itemLineCreate.Name, itemLines[0].Name);
        Xunit.Assert.Equal(_itemLineCreate.Description, itemLines[0].Description);
    }

    [Fact, TestPriority(3)]
    public async Task GetItemLine()
    {
        var response = await _client.GetAsync($"{requestUri}/1");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemLine? itemLine = await response.Content.ReadFromJsonAsync<ItemLine>();
        Xunit.Assert.NotNull(itemLine);
        Xunit.Assert.Equal(_itemLineCreate.Id, itemLine.Id);
        Xunit.Assert.Equal(_itemLineCreate.Name, itemLine.Name);
        Xunit.Assert.Equal(_itemLineCreate.Description, itemLine.Description);
    }

    [Fact, TestPriority(4)]
    public async Task GetItemLineNotFound()
    {
        var response = await _client.GetAsync($"{requestUri}/3");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task UpdateItemLine()
    {
        // Description is updated from "Never gonna give you up" to "Never gonna let you down"
        var response = await _client.PutAsJsonAsync($"{requestUri}/1", _itemLinePut);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task GetUpdatedItemLine()
    {
        var response = await _client.GetAsync($"{requestUri}/1");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemLine? itemLine = await response.Content.ReadFromJsonAsync<ItemLine>();
        Xunit.Assert.NotNull(itemLine);
        Xunit.Assert.Equal(_itemLinePut.Id, itemLine.Id);
        Xunit.Assert.Equal(_itemLinePut.Name, itemLine.Name);
        Xunit.Assert.Equal(_itemLinePut.Description, itemLine.Description);
    }
    [Fact, TestPriority(7)]
    public async Task GetItemLineItemsNotFound()
    {
        var response = await _client.GetAsync("/api/v1/item_lines/3/items");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact, TestPriority(8)]
    public async Task GetItemLineItems()
    {
        var response = await _client.GetAsync("/api/v1/item_lines/1/items");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Item[]? items = await response.Content.ReadFromJsonAsync<Item[]>();
        Xunit.Assert.NotNull(items);
        Xunit.Assert.Equal(_item.Uid, items[0].Uid);
        Xunit.Assert.Equal(_item.Code, items[0].Code);
        Xunit.Assert.Equal(_item.Description, items[0].Description);
        Xunit.Assert.Equal(_item.ShortDescription, items[0].ShortDescription);
        Xunit.Assert.Equal(_item.UpcCode, items[0].UpcCode);
        Xunit.Assert.Equal(_item.ModelNumber, items[0].ModelNumber);
        Xunit.Assert.Equal(_item.CommodityCode, items[0].CommodityCode);
        Xunit.Assert.Equal(_item.ItemLine, items[0].ItemLine);
        Xunit.Assert.Equal(_item.ItemGroup, items[0].ItemGroup);
        Xunit.Assert.Equal(_item.ItemType, items[0].ItemType);
        Xunit.Assert.Equal(_item.UnitPurchaseQuantity, items[0].UnitPurchaseQuantity);
        Xunit.Assert.Equal(_item.UnitOrderQuantity, items[0].UnitOrderQuantity);
        Xunit.Assert.Equal(_item.PackOrderQuantity, items[0].PackOrderQuantity);
        Xunit.Assert.Equal(_item.SupplierId, items[0].SupplierId);
        Xunit.Assert.Equal(_item.SupplierCode, items[0].SupplierCode);
        Xunit.Assert.Equal(_item.SupplierPartNumber, items[0].SupplierPartNumber);
    }

    [Fact, TestPriority(9)]
    public async Task GetItemLineNotFoundAgain()
    {
        var response = await _client.GetAsync($"{requestUri}/2");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(10)]
    public async Task DeleteItemLine()
    {
        var response = await _client.DeleteAsync($"{requestUri}/1");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", result);
    }

    [Fact, TestPriority(11)]
    public async Task GetItemLineEmpty()
    {
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(12)]
    public async Task DeleteItem()
    {
        var response = await _client.DeleteAsync($"{requestUriItem}/P000005");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
