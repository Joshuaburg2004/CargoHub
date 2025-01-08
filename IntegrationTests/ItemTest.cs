using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit;
using CargoHubAlt.Models;
using System.Net.Http.Json;
namespace IntegrationTests;

[TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]

public class ItemIntegratieTest : BaseTest
{

    public static Item TestItem = new("P000001", "sjQ23408K", "Face-to-face clear-thinking complexity",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 0, 0, 0, 0, 0, 0, 0, "SUP423", "E-86805-uTM");

    public static Item TestPutItem = new("P000001", "item", "item for testing put",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 0, 0, 0, 0, 0, 0, 0, "SUP423", "E-86805-uTM");

    public string requestUri = "/api/v1/items";

    public Inventory TestInventory = new(5, "P000001", "test", "63-OFFTq0T", new List<int>() { 3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817 }, 40, 40, 40, 40, 40, 10);


    public ItemIntegratieTest() { }

    [Fact, TestPriority(0)]
    public async Task GetAllItemsEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync(requestUri);
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<Item>? returnedlist = JsonSerializer.Deserialize<List<Item>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<Item>>(returnedlist);
        Xunit.Assert.Empty(returnedlist);
    }

    [Fact, TestPriority(1)]
    public async Task GetOneItemEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001");
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact, TestPriority(2)]
    public async Task PostItem()
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(requestUri, TestItem);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Xunit.Assert.Equal("P000001", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(3)]
    public async Task PostItemNull()
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync<Item>(requestUri, null!);
        Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Xunit.Assert.Equal("This is not an item", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(4)]
    public async Task PostItemExists()
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(requestUri, TestItem);
        Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Xunit.Assert.Equal($"Item with Uid {TestItem.Uid} already exists", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(5)]
    public async Task GetItemOne()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001");

        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Item? ToCompare = await response.Content.ReadFromJsonAsync<Item>();
        Xunit.Assert.IsType<Item>(ToCompare);
        Xunit.Assert.Equal(TestItem.Uid, ToCompare.Uid);
        Xunit.Assert.Equal(TestItem.Code, ToCompare.Code);
        Xunit.Assert.Equal(TestItem.Description, ToCompare.Description);
        Xunit.Assert.Equal(TestItem.ShortDescription, ToCompare.ShortDescription);
        Xunit.Assert.Equal(TestItem.UpcCode, ToCompare.UpcCode);
        Xunit.Assert.Equal(TestItem.ModelNumber, ToCompare.ModelNumber);
        Xunit.Assert.Equal(TestItem.CommodityCode, ToCompare.CommodityCode);
        Xunit.Assert.Equal(TestItem.ItemLine, ToCompare.ItemLine);
        Xunit.Assert.Equal(TestItem.ItemGroup, ToCompare.ItemGroup);
        Xunit.Assert.Equal(TestItem.ItemType, ToCompare.ItemType);
        Xunit.Assert.Equal(TestItem.UnitPurchaseQuantity, ToCompare.UnitPurchaseQuantity);
        Xunit.Assert.Equal(TestItem.UnitOrderQuantity, ToCompare.UnitOrderQuantity);
        Xunit.Assert.Equal(TestItem.PackOrderQuantity, ToCompare.PackOrderQuantity);
        Xunit.Assert.Equal(TestItem.SupplierId, ToCompare.SupplierId);
        Xunit.Assert.Equal(TestItem.SupplierCode, ToCompare.SupplierCode);
        Xunit.Assert.Equal(TestItem.SupplierPartNumber, ToCompare.SupplierPartNumber);
    }

    [Fact, TestPriority(6)]
    public async Task GetAllItemsOne()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        List<Item>? responselist = await response.Content.ReadFromJsonAsync<List<Item>>();
        Xunit.Assert.IsType<List<Item>>(responselist);
        Xunit.Assert.Single(responselist);

        Item ToCompare = responselist[0];
        Xunit.Assert.IsType<Item>(ToCompare);
        Xunit.Assert.Equal(TestItem.Uid, ToCompare.Uid);
        Xunit.Assert.Equal(TestItem.Code, ToCompare.Code);
        Xunit.Assert.Equal(TestItem.Description, ToCompare.Description);
        Xunit.Assert.Equal(TestItem.ShortDescription, ToCompare.ShortDescription);
        Xunit.Assert.Equal(TestItem.UpcCode, ToCompare.UpcCode);
        Xunit.Assert.Equal(TestItem.ModelNumber, ToCompare.ModelNumber);
        Xunit.Assert.Equal(TestItem.CommodityCode, ToCompare.CommodityCode);
        Xunit.Assert.Equal(TestItem.ItemLine, ToCompare.ItemLine);
        Xunit.Assert.Equal(TestItem.ItemGroup, ToCompare.ItemGroup);
        Xunit.Assert.Equal(TestItem.ItemType, ToCompare.ItemType);
        Xunit.Assert.Equal(TestItem.UnitPurchaseQuantity, ToCompare.UnitPurchaseQuantity);
        Xunit.Assert.Equal(TestItem.UnitOrderQuantity, ToCompare.UnitOrderQuantity);
        Xunit.Assert.Equal(TestItem.PackOrderQuantity, ToCompare.PackOrderQuantity);
        Xunit.Assert.Equal(TestItem.SupplierId, ToCompare.SupplierId);
        Xunit.Assert.Equal(TestItem.SupplierCode, ToCompare.SupplierCode);
        Xunit.Assert.Equal(TestItem.SupplierPartNumber, ToCompare.SupplierPartNumber);
    }



    [Fact, TestPriority(7)]
    public async Task UpdateItems()
    {
        HttpResponseMessage response = await _client.PutAsJsonAsync($"{requestUri}/P000001", TestPutItem);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(8)]
    public async Task UpdateItemsNotFound()
    {
        HttpResponseMessage response = await _client.PutAsJsonAsync($"{requestUri}/NotFound", TestPutItem);
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Xunit.Assert.Equal($"Item with UID NotFound not found", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(9)]
    public async Task GetUpdatedItemTest()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001");
        var responseContent = await response.Content.ReadAsStringAsync();

        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Item? ToCompare = await response.Content.ReadFromJsonAsync<Item>();
        Xunit.Assert.IsType<Item>(ToCompare);
        Xunit.Assert.Equal(TestPutItem.Uid, ToCompare.Uid);
        Xunit.Assert.Equal(TestPutItem.Code, ToCompare.Code);
        Xunit.Assert.Equal(TestPutItem.Description, ToCompare.Description);
        Xunit.Assert.Equal(TestPutItem.ShortDescription, ToCompare.ShortDescription);
        Xunit.Assert.Equal(TestPutItem.UpcCode, ToCompare.UpcCode);
        Xunit.Assert.Equal(TestPutItem.ModelNumber, ToCompare.ModelNumber);
        Xunit.Assert.Equal(TestPutItem.CommodityCode, ToCompare.CommodityCode);
        Xunit.Assert.Equal(TestPutItem.ItemLine, ToCompare.ItemLine);
        Xunit.Assert.Equal(TestPutItem.ItemGroup, ToCompare.ItemGroup);
        Xunit.Assert.Equal(TestPutItem.ItemType, ToCompare.ItemType);
        Xunit.Assert.Equal(TestPutItem.UnitPurchaseQuantity, ToCompare.UnitPurchaseQuantity);
        Xunit.Assert.Equal(TestPutItem.UnitOrderQuantity, ToCompare.UnitOrderQuantity);
        Xunit.Assert.Equal(TestPutItem.PackOrderQuantity, ToCompare.PackOrderQuantity);
        Xunit.Assert.Equal(TestPutItem.SupplierId, ToCompare.SupplierId);
        Xunit.Assert.Equal(TestPutItem.SupplierCode, ToCompare.SupplierCode);
        Xunit.Assert.Equal(TestItem.SupplierPartNumber, ToCompare.SupplierPartNumber);
    }

    [Fact, TestPriority(10)]
    public async Task GetItemInventories()
    {

        HttpResponseMessage getemptyresponse = await _client.GetAsync($"{requestUri}/P000001/inventory");
        Xunit.Assert.Equal(HttpStatusCode.NotFound, getemptyresponse.StatusCode);
        Xunit.Assert.Equal("Inventory for item with UID P000001 not found", await getemptyresponse.Content.ReadAsStringAsync());



        HttpResponseMessage postInventoryResponse = await _client.PostAsJsonAsync("/api/v1/inventories", TestInventory);

        Xunit.Assert.Equal(HttpStatusCode.Created, postInventoryResponse.StatusCode);

        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001/inventory");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        List<Inventory>? inventoryresponse = await response.Content.ReadFromJsonAsync<List<Inventory>>();

        Xunit.Assert.IsType<List<Inventory>>(inventoryresponse);
        Xunit.Assert.Single(inventoryresponse);
        Inventory inventoryCompared = inventoryresponse[0];

        Xunit.Assert.Equal(TestInventory.Id, inventoryCompared.Id);
        Xunit.Assert.Equal(TestInventory.ItemId, inventoryCompared.ItemId);
        Xunit.Assert.Equal(TestInventory.Description, inventoryCompared.Description);
        Xunit.Assert.Equal(TestInventory.ItemReference, inventoryCompared.ItemReference);
        Xunit.Assert.Equal(TestInventory.Locations, inventoryCompared.Locations);
        Xunit.Assert.Equal(TestInventory.TotalOnHand, inventoryCompared.TotalOnHand);
        Xunit.Assert.Equal(TestInventory.TotalExpected, inventoryCompared.TotalExpected);
        Xunit.Assert.Equal(TestInventory.TotalOrdered, inventoryCompared.TotalOrdered);
        Xunit.Assert.Equal(TestInventory.TotalAllocated, inventoryCompared.TotalAllocated);
        Xunit.Assert.Equal(TestInventory.TotalAvailable, inventoryCompared.TotalAvailable);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/inventories/5");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(11)]
    public async Task GetItemInventoriesTotal()
    {
        HttpResponseMessage getemptyresponse = await _client.GetAsync($"{requestUri}/P000001/inventory/totals");
        Xunit.Assert.Equal(HttpStatusCode.OK, getemptyresponse.StatusCode);
        Dictionary<string, int>? emptyresponseContent = await getemptyresponse.Content.ReadFromJsonAsync<Dictionary<string, int>>();

        Xunit.Assert.IsType<Dictionary<string, int>>(emptyresponseContent);

        Xunit.Assert.Equal(0, emptyresponseContent["total_expected"]);
        Xunit.Assert.Equal(0, emptyresponseContent["total_ordered"]);
        Xunit.Assert.Equal(0, emptyresponseContent["total_allocated"]);
        Xunit.Assert.Equal(0, emptyresponseContent["total_available"]);


        HttpResponseMessage postInventoryResponse = await _client.PostAsJsonAsync("/api/v1/inventories", TestInventory);

        Xunit.Assert.Equal(HttpStatusCode.Created, postInventoryResponse.StatusCode);


        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001/inventory/totals");


        Dictionary<string, int>? updateresponsecontent = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();

        Xunit.Assert.IsType<Dictionary<string, int>>(updateresponsecontent);

        Xunit.Assert.Equal(TestInventory.TotalExpected, updateresponsecontent["total_expected"]);
        Xunit.Assert.Equal(TestInventory.TotalOrdered, updateresponsecontent["total_ordered"]);
        Xunit.Assert.Equal(TestInventory.TotalAllocated, updateresponsecontent["total_allocated"]);
        Xunit.Assert.Equal(TestInventory.TotalAvailable, updateresponsecontent["total_available"]);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/inventories/5");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(12)]
    public async Task GetItemInventoriesNotFound()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/NotFound/inventory");
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(12)]
    public async Task DeleteItem()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"{requestUri}/P000001");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadFromJsonAsync<Item>();

        Xunit.Assert.IsType<Item>(responseContent);
        Xunit.Assert.Equal(TestPutItem.Uid, responseContent.Uid);
        Xunit.Assert.Equal(TestPutItem.Code, responseContent.Code);
        Xunit.Assert.Equal(TestPutItem.Description, responseContent.Description);
        Xunit.Assert.Equal(TestPutItem.ShortDescription, responseContent.ShortDescription);
        Xunit.Assert.Equal(TestPutItem.UpcCode, responseContent.UpcCode);
        Xunit.Assert.Equal(TestPutItem.ModelNumber, responseContent.ModelNumber);
        Xunit.Assert.Equal(TestPutItem.CommodityCode, responseContent.CommodityCode);
        Xunit.Assert.Equal(TestPutItem.ItemLine, responseContent.ItemLine);
        Xunit.Assert.Equal(TestPutItem.ItemGroup, responseContent.ItemGroup);
        Xunit.Assert.Equal(TestPutItem.ItemType, responseContent.ItemType);
        Xunit.Assert.Equal(TestPutItem.UnitPurchaseQuantity, responseContent.UnitPurchaseQuantity);
        Xunit.Assert.Equal(TestPutItem.UnitOrderQuantity, responseContent.UnitOrderQuantity);
        Xunit.Assert.Equal(TestPutItem.PackOrderQuantity, responseContent.PackOrderQuantity);
        Xunit.Assert.Equal(TestPutItem.SupplierId, responseContent.SupplierId);
        Xunit.Assert.Equal(TestPutItem.SupplierCode, responseContent.SupplierCode);
        Xunit.Assert.Equal(TestItem.SupplierPartNumber, responseContent.SupplierPartNumber);
    }

    [Fact, TestPriority(13)]
    public async Task DeleteItemNotFound()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"{requestUri}/P000001");
        Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("Item with UID P000001 not found", responseContent);
    }

    [Fact, TestPriority(14)]
    public async Task GetItemEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
    }

    [Fact, TestPriority(15)]
    public async Task GetAllItemsEmptyAfterDelete()
    {
        HttpResponseMessage response = await _client.GetAsync(requestUri);
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<Item>? returnedlist = JsonSerializer.Deserialize<List<Item>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<Item>>(returnedlist);
        Xunit.Assert.Empty(returnedlist);
    }
}
