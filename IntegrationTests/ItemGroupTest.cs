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

public class ItemGroupTest : BaseTest
{
    private ItemGroup _itemGroupCreate = new ItemGroup(1, "Book", "Never gonna give you up");
    private ItemGroup _itemGroupPut = new ItemGroup(1, "Book", "Never gonna let you down");
    private Item _item = new Item("P000001", "Book", "Never gonna give you up", "Never gonna", "123456789", "123456789", "123456789", 1, 1, 1, 1, 1, 1, 1, "123456789", "123456789");
    public ItemGroupTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        _createItemGroup();
        _createItem();
    }

    private async Task _createItemGroup()
    {
        await _client.PostAsJsonAsync("/api/v1/item_groups", _itemGroupCreate);
    }

    private async Task _createItem()
    {
        await _client.PostAsJsonAsync("/api/v1/items", _item);
    }

    [Fact, TestPriority(0)]
    public async Task GetAllItemGroupsOne()
    {
        var requestUri = "/api/v1/item_groups";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        List<ItemGroup>? itemGroups = await response.Content.ReadFromJsonAsync<List<ItemGroup>>();
        Xunit.Assert.NotNull(itemGroups);
        Xunit.Assert.Equal(_itemGroupCreate.Id, itemGroups[0].Id);
        Xunit.Assert.Equal(_itemGroupCreate.Name, itemGroups[0].Name);
        Xunit.Assert.Equal(_itemGroupCreate.Description, itemGroups[0].Description);
    }

    [Fact, TestPriority(1)]
    public async Task GetItemGroup()
    {
        var requestUri = "/api/v1/item_groups/1";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemGroup? itemGroup = await response.Content.ReadFromJsonAsync<ItemGroup>();
        Xunit.Assert.NotNull(itemGroup);
        Xunit.Assert.Equal(_itemGroupCreate.Id, itemGroup.Id);
        Xunit.Assert.Equal(_itemGroupCreate.Name, itemGroup.Name);
        Xunit.Assert.Equal(_itemGroupCreate.Description, itemGroup.Description);
    }

    [Fact, TestPriority(2)]
    public async Task UpdateItemGroup()
    {
        // Description is updated from "Never gonna give you up" to "Never gonna let you down"
        var requestUri = "/api/v1/item_groups/1";
        var response = await _client.PutAsJsonAsync(requestUri, _itemGroupPut);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task GetUpdatedItemGroup()
    {
        var requestUri = "/api/v1/item_groups/1";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ItemGroup? itemGroup = await response.Content.ReadFromJsonAsync<ItemGroup>();
        Xunit.Assert.NotNull(itemGroup);
        Xunit.Assert.Equal(_itemGroupPut.Id, itemGroup.Id);
        Xunit.Assert.Equal(_itemGroupPut.Name, itemGroup.Name);
        Xunit.Assert.Equal(_itemGroupPut.Description, itemGroup.Description);
    }

    [Fact, TestPriority(4)]
    public async Task GetItemGroupItems()
    {
        var requestUri = "/api/v1/item_groups/1/items";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        List<Item>? items = await response.Content.ReadFromJsonAsync<List<Item>>();
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

        // Delete item
        var deleteRequestUri = "/api/v1/items/P000001";
        var responsedelete = await _client.DeleteAsync(deleteRequestUri);
        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task GetWrongItemGroup()
    {
        var requestUri = "/api/v1/item_groups/2";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(result);
        Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
    }

    [Fact, TestPriority(6)]
    public async Task DeleteItemGroup()
    {
        var requestUri = "/api/v1/item_groups/1";
        var response = await _client.DeleteAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", result);
    }

    [Fact, TestPriority(7)]
    public async Task GetItemGroupEmpty()
    {
        var requestUri = "/api/v1/item_groups";
        var response = await _client.GetAsync(requestUri);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
