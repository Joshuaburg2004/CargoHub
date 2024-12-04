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

public class ItemTypeTest : BaseTest
{
    private ItemType _itemTypeCreate = new ItemType(1, "Laptop", "Never gonna give you up");
    private ItemType _itemTypePut = new ItemType(1, "Laptop", "Never gonna let you down");
    private Item _item = new Item("P000005", "Laptop", "Never gonna give you up", "Never gonna", "123456789", "123456789", "123456789", 1, 1, 1, 1, 1, 1, 1, "123456789", "123456789");

    public ItemTypeTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    { }

    [Fact, TestPriority(0)]
    public async Task CreateItemType()
    {
        var requestUri = "/api/v1/item_types";
        var response = await _client.PostAsJsonAsync(requestUri, _itemTypeCreate);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact, TestPriority(1)]
    public async Task CreateItem()
    {
        var requestUri = "/api/v1/items";
        var response = await _client.PostAsJsonAsync(requestUri, _item);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact, TestPriority(2)]
    public async Task GetAllItemTypesOne()
    {
        var requestUri = "/api/v1/item_types";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemType[]? itemTypes = await response.Content.ReadFromJsonAsync<ItemType[]>();
        Xunit.Assert.NotNull(itemTypes);
        Xunit.Assert.Equal(_itemTypeCreate.Id, itemTypes[0].Id);
        Xunit.Assert.Equal(_itemTypeCreate.Name, itemTypes[0].Name);
        Xunit.Assert.Equal(_itemTypeCreate.Description, itemTypes[0].Description);
    }

    [Fact, TestPriority(3)]
    public async Task GetItemType()
    {
        var requestUri = "/api/v1/item_types/1";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemType? itemType = await response.Content.ReadFromJsonAsync<ItemType>();
        Xunit.Assert.NotNull(itemType);
        Xunit.Assert.Equal(_itemTypeCreate.Id, itemType.Id);
        Xunit.Assert.Equal(_itemTypeCreate.Name, itemType.Name);
        Xunit.Assert.Equal(_itemTypeCreate.Description, itemType.Description);
    }

    [Fact, TestPriority(4)]
    public async Task GetItemTypeNotFound()
    {
        var requestUri = "/api/v1/item_types/3";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task UpdateItemType()
    {
        // Description is updated from "Never gonna give you up" to "Never gonna let you down"
        var requestUri = "/api/v1/item_types/1";
        var response = await _client.PutAsJsonAsync(requestUri, _itemTypePut);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task GetUpdatedItemType()
    {
        var requestUri = "/api/v1/item_types/1";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemType? itemType = await response.Content.ReadFromJsonAsync<ItemType>();
        Xunit.Assert.NotNull(itemType);
        Xunit.Assert.Equal(_itemTypePut.Id, itemType.Id);
        Xunit.Assert.Equal(_itemTypePut.Name, itemType.Name);
        Xunit.Assert.Equal(_itemTypePut.Description, itemType.Description);
    }
    [Fact, TestPriority(7)]
    public async Task GetItemTypeItemsNotFound()
    {
        var requestUri = "/api/v1/item_types/3/items";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact, TestPriority(8)]
    public async Task GetItemTypeItems()
    {
        var requestUri = "/api/v1/item_types/1/items";
        var response = await _client.GetAsync(requestUri);
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
        Xunit.Assert.Equal(_item.ItemType, items[0].ItemType);
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
    public async Task GetItemTypeNotFoundAgain()
    {
        var requestUri = "/api/v1/item_types/2";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(10)]
    public async Task DeleteItemType()
    {
        var requestUri = "/api/v1/item_types/1";
        var response = await _client.DeleteAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", result);
    }

    [Fact, TestPriority(11)]
    public async Task GetItemTypeEmpty()
    {
        var requestUri = "/api/v1/item_types";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(12)]
    public async Task DeleteItem()
    {
        var requestUri = "/api/v1/items/P000005";
        var response = await _client.DeleteAsync(requestUri);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}