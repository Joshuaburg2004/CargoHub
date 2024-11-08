using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit;
using PythonTests.models;

namespace PythonTests;

[TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]

public class ItemIntegratieTest : BaseTest
{

    public static PythonTests.models.Item TestItem = new("P000001", "sjQ23408K", "Face-to-face clear-thinking complexity",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 0, 0,0,0,0,0,0,"SUP423", "E-86805-uTM");

    public static string testTypeJson {get => JsonSerializer.Serialize(TestItem);}

    public static PythonTests.models.Item TestPutItem = new("P000001", "item", "item for testing put",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 0, 0,0,0,0,0,0,"SUP423", "E-86805-uTM");

    public string requestUri = "/api/v1/items";

    public PythonTests.models.Inventory TestInventory = new(5, "P000001", "test", "63-OFFTq0T", new List<int>(){3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817}, 40, 40, 40, 40, 40);


    public ItemIntegratieTest(): base()
    {}

    [Fact, TestPriority(0)]
    public async Task GetAllItemsEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync(requestUri);
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<PythonTests.models.Item>? returnedlist = JsonSerializer.Deserialize<List<PythonTests.models.Item>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<PythonTests.models.Item>>(returnedlist);
        Xunit.Assert.Empty(returnedlist);
    }

    
    [Fact, TestPriority(1)]
    public async Task PostItem()
    {
        HttpResponseMessage response = await _client.PostAsync(requestUri, new StringContent(testTypeJson, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
    }
    
    
    
    [Fact, TestPriority(2)]
    public async Task GetItemOne()
    {
        
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        PythonTests.models.Item? ToCompare = JsonSerializer.Deserialize<PythonTests.models.Item>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<PythonTests.models.Item>(ToCompare);
        Xunit.Assert.Equal(TestItem.uid, ToCompare.uid);
        Xunit.Assert.Equal(TestItem.code, ToCompare.code);
        Xunit.Assert.Equal(TestItem.description, ToCompare.description);
        Xunit.Assert.Equal(TestItem.short_description, ToCompare.short_description);
        Xunit.Assert.Equal(TestItem.upc_code, ToCompare.upc_code);
        Xunit.Assert.Equal(TestItem.model_number, ToCompare.model_number);
        Xunit.Assert.Equal(TestItem.commodity_code, ToCompare.commodity_code);
        Xunit.Assert.Equal(TestItem.item_line, ToCompare.item_line);
        Xunit.Assert.Equal(TestItem.item_group, ToCompare.item_group);
        Xunit.Assert.Equal(TestItem.item_type, ToCompare.item_type);
        Xunit.Assert.Equal(TestItem.unit_purchase_quantity, ToCompare.unit_purchase_quantity);
        Xunit.Assert.Equal(TestItem.unit_order_quantity, ToCompare.unit_order_quantity);
        Xunit.Assert.Equal(TestItem.pack_order_quantity, ToCompare.pack_order_quantity);
        Xunit.Assert.Equal(TestItem.supplier_id, ToCompare.supplier_id);
        Xunit.Assert.Equal(TestItem.supplier_code, ToCompare.supplier_code);
        Xunit.Assert.Equal(TestItem.supplier_part_number, ToCompare.supplier_part_number);
    }

    [Fact, TestPriority(2)]
    public async Task GetAllItemsOne()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        List<PythonTests.models.Item>? responselist = JsonSerializer.Deserialize<List<PythonTests.models.Item>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<PythonTests.models.Item>>(responselist);
        Xunit.Assert.Single(responselist);

        PythonTests.models.Item ToCompare = responselist[0];

        Xunit.Assert.Equal(TestItem.uid, ToCompare.uid);
        Xunit.Assert.Equal(TestItem.code, ToCompare.code);
        Xunit.Assert.Equal(TestItem.description, ToCompare.description);
        Xunit.Assert.Equal(TestItem.short_description, ToCompare.short_description);
        Xunit.Assert.Equal(TestItem.upc_code, ToCompare.upc_code);
        Xunit.Assert.Equal(TestItem.model_number, ToCompare.model_number);
        Xunit.Assert.Equal(TestItem.commodity_code, ToCompare.commodity_code);
        Xunit.Assert.Equal(TestItem.item_line, ToCompare.item_line);
        Xunit.Assert.Equal(TestItem.item_group, ToCompare.item_group);
        Xunit.Assert.Equal(TestItem.item_type, ToCompare.item_type);
        Xunit.Assert.Equal(TestItem.unit_purchase_quantity, ToCompare.unit_purchase_quantity);
        Xunit.Assert.Equal(TestItem.unit_order_quantity, ToCompare.unit_order_quantity);
        Xunit.Assert.Equal(TestItem.pack_order_quantity, ToCompare.pack_order_quantity);
        Xunit.Assert.Equal(TestItem.supplier_id, ToCompare.supplier_id);
        Xunit.Assert.Equal(TestItem.supplier_code, ToCompare.supplier_code);
        Xunit.Assert.Equal(TestItem.supplier_part_number, ToCompare.supplier_part_number);
    }



    [Fact, TestPriority(3)]
    public async Task UpdateItems()
    {
        string toSend = JsonSerializer.Serialize(TestPutItem);
        HttpResponseMessage response = await _client.PutAsync($"{requestUri}/P000001", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
        
    }

    [Fact, TestPriority(4)]
    public async Task GetUpdatedItemTest()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001");
        var responseContent = await response.Content.ReadAsStringAsync();
        PythonTests.models.Item? itemtypeafterupdate = JsonSerializer.Deserialize<PythonTests.models.Item>(responseContent);

        Xunit.Assert.IsType<PythonTests.models.Item>(itemtypeafterupdate);

        Xunit.Assert.Equal(TestPutItem.uid, itemtypeafterupdate.uid);
        Xunit.Assert.Equal(TestPutItem.code, itemtypeafterupdate.code);
        Xunit.Assert.Equal(TestPutItem.description, itemtypeafterupdate.description);
        Xunit.Assert.Equal(TestPutItem.short_description, itemtypeafterupdate.short_description);
        Xunit.Assert.Equal(TestPutItem.upc_code, itemtypeafterupdate.upc_code);
        Xunit.Assert.Equal(TestPutItem.model_number, itemtypeafterupdate.model_number);
        Xunit.Assert.Equal(TestPutItem.commodity_code, itemtypeafterupdate.commodity_code);
        Xunit.Assert.Equal(TestPutItem.item_line, itemtypeafterupdate.item_line);
        Xunit.Assert.Equal(TestPutItem.item_group, itemtypeafterupdate.item_group);
        Xunit.Assert.Equal(TestPutItem.item_type, itemtypeafterupdate.item_type);
        Xunit.Assert.Equal(TestPutItem.unit_purchase_quantity, itemtypeafterupdate.unit_purchase_quantity);
        Xunit.Assert.Equal(TestPutItem.unit_order_quantity, itemtypeafterupdate.unit_order_quantity);
        Xunit.Assert.Equal(TestPutItem.pack_order_quantity, itemtypeafterupdate.pack_order_quantity);
        Xunit.Assert.Equal(TestPutItem.supplier_id, itemtypeafterupdate.supplier_id);
        Xunit.Assert.Equal(TestPutItem.supplier_code, itemtypeafterupdate.supplier_code);
        Xunit.Assert.Equal(TestPutItem.supplier_part_number, itemtypeafterupdate.supplier_part_number);
    }

    [Fact, TestPriority(4)]
    public async Task GetItemInventories()
    {
        HttpResponseMessage getemptyresponse = await _client.GetAsync($"{requestUri}/P000001/inventory");
        Xunit.Assert.Equal(HttpStatusCode.OK, getemptyresponse.StatusCode);
        Xunit.Assert.Equal("[]", await getemptyresponse.Content.ReadAsStringAsync());

        string Tosend = JsonSerializer.Serialize(TestInventory);


        HttpResponseMessage postInventoryResponse = await _client.PostAsync("/api/v1/inventories", new StringContent(Tosend, System.Text.Encoding.UTF8, "application/json"));

        Xunit.Assert.Equal(HttpStatusCode.Created, postInventoryResponse.StatusCode);


        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001/inventory");
        var responseContent = await response.Content.ReadAsStringAsync();
        List<PythonTests.models.Inventory>? inventoryresponse = JsonSerializer.Deserialize<List<PythonTests.models.Inventory>>(responseContent);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Xunit.Assert.IsType<List<PythonTests.models.Inventory>>(inventoryresponse);
        Xunit.Assert.Single(inventoryresponse);
        PythonTests.models.Inventory inventoryCompared = inventoryresponse[0];
                

        Xunit.Assert.Equal(TestInventory.id, inventoryCompared.id);
        Xunit.Assert.Equal(TestInventory.item_id, inventoryCompared.item_id);
        Xunit.Assert.Equal(TestInventory.description, inventoryCompared.description);
        Xunit.Assert.Equal(TestInventory.item_reference, inventoryCompared.item_reference);
        Xunit.Assert.Equal(TestInventory.locations, inventoryCompared.locations);
        Xunit.Assert.Equal(TestInventory.total_on_hand, inventoryCompared.total_on_hand);
        Xunit.Assert.Equal(TestInventory.total_expected, inventoryCompared.total_expected);
        Xunit.Assert.Equal(TestInventory.total_ordered, inventoryCompared.total_ordered);
        Xunit.Assert.Equal(TestInventory.total_allocated, inventoryCompared.total_allocated);
        Xunit.Assert.Equal(TestInventory.total_available, inventoryCompared.total_available);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/inventories/5");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task GetItemInventoriesTotal()
    {
        HttpResponseMessage getemptyresponse = await _client.GetAsync($"{requestUri}/P000001/inventory/totals");
        Xunit.Assert.Equal(HttpStatusCode.OK, getemptyresponse.StatusCode);
        string emptyresponseContent = await getemptyresponse.Content.ReadAsStringAsync();
        InventoryTotals? emptyinventoryresponse = JsonSerializer.Deserialize<InventoryTotals>(emptyresponseContent);
        Xunit.Assert.IsType<InventoryTotals>(emptyinventoryresponse);
        
        Xunit.Assert.Equal(0, emptyinventoryresponse.total_expected);
        Xunit.Assert.Equal(0, emptyinventoryresponse.total_ordered);
        Xunit.Assert.Equal(0, emptyinventoryresponse.total_allocated);
        Xunit.Assert.Equal(0, emptyinventoryresponse.total_available);

        string Tosend = JsonSerializer.Serialize(TestInventory);


        HttpResponseMessage postInventoryResponse = await _client.PostAsync("/api/v1/inventories", new StringContent(Tosend, System.Text.Encoding.UTF8, "application/json"));

        Xunit.Assert.Equal(HttpStatusCode.Created, postInventoryResponse.StatusCode);


        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/P000001/inventory/totals");
        string responseContent = await response.Content.ReadAsStringAsync();
        InventoryTotals? inventoryresponse = JsonSerializer.Deserialize<InventoryTotals>(responseContent);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        Xunit.Assert.IsType<InventoryTotals>(inventoryresponse);

        Xunit.Assert.Equal(TestInventory.total_expected, inventoryresponse.total_expected);
        Xunit.Assert.Equal(TestInventory.total_ordered, inventoryresponse.total_ordered);
        Xunit.Assert.Equal(TestInventory.total_allocated, inventoryresponse.total_allocated);
        Xunit.Assert.Equal(TestInventory.total_available, inventoryresponse.total_available);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/inventories/5");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task DeleteItem()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"{requestUri}/P000001");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("", responseContent);
    }

    [Fact, TestPriority(7)]
    public async Task GetItemEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    }
}
