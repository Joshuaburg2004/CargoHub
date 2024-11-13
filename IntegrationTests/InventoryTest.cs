using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;
using PythonTests.models;

namespace PythonTests;

[TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
public class InventoryIntegratieTest : BaseTest
{

    public string requestUri = "/api/v1/inventories";

    public Inventory TestInventory = new(1, "P000001", "test", "63-OFFTq0T", new List<int>(){3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817}, 40, 40, 40, 40, 40);

    public Inventory TestInventoryPut = new(1, "P000001", "test", "hopeful", new List<int>(){3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817}, 60, 60, 60, 60, 60);


    public InventoryIntegratieTest(): base()
    {}

    [Fact, TestPriority(0)]
    public async Task GetAllInventoriesEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync(requestUri);
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<Inventory>? returnedlist = JsonSerializer.Deserialize<List<Inventory>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<Inventory>>(returnedlist);
        Xunit.Assert.Empty(returnedlist);
    }

    
    [Fact, TestPriority(1)]
    public async Task PostInventory()
    {
        string testinventoryjson = JsonSerializer.Serialize(TestInventory);
        HttpResponseMessage response = await _client.PostAsync(requestUri, new StringContent(testinventoryjson, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
    }
    
    
    
    [Fact, TestPriority(2)]
    public async Task GetInventoryOne()
    {
        
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Inventory? inventoryCompared = JsonSerializer.Deserialize<Inventory>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<Inventory>(inventoryCompared);
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

    }

    [Fact, TestPriority(3)]
    public async Task GetAllInventorysOne()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        List<Inventory>? responselist = JsonSerializer.Deserialize<List<Inventory>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<Inventory>>(responselist);
        Xunit.Assert.Single(responselist);

        Inventory inventoryCompared = responselist[0];

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

    }



    [Fact, TestPriority(4)]
    public async Task UpdateInventorys()
    {
        string toSend = JsonSerializer.Serialize(TestInventoryPut);
        HttpResponseMessage response = await _client.PutAsync($"{requestUri}/1", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
        
    }

    [Fact, TestPriority(5)]
    public async Task GetUpdatedInventoryTest()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/1");
        var responseContent = await response.Content.ReadAsStringAsync();
        Inventory? Inventoryafterupdate = JsonSerializer.Deserialize<Inventory>(responseContent);

        Xunit.Assert.IsType<Inventory>(Inventoryafterupdate);
        Xunit.Assert.Equal(TestInventoryPut.id, Inventoryafterupdate.id);
        Xunit.Assert.Equal(TestInventoryPut.item_id, Inventoryafterupdate.item_id);
        Xunit.Assert.Equal(TestInventoryPut.description, Inventoryafterupdate.description);
        Xunit.Assert.Equal(TestInventoryPut.item_reference, Inventoryafterupdate.item_reference);
        Xunit.Assert.Equal(TestInventoryPut.locations, Inventoryafterupdate.locations);
        Xunit.Assert.Equal(TestInventoryPut.total_on_hand, Inventoryafterupdate.total_on_hand);
        Xunit.Assert.Equal(TestInventoryPut.total_expected, Inventoryafterupdate.total_expected);
        Xunit.Assert.Equal(TestInventoryPut.total_ordered, Inventoryafterupdate.total_ordered);
        Xunit.Assert.Equal(TestInventoryPut.total_allocated, Inventoryafterupdate.total_allocated);
        Xunit.Assert.Equal(TestInventoryPut.total_available, Inventoryafterupdate.total_available);
    }


    [Fact, TestPriority(6)]
    public async Task DeleteInventory()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"{requestUri}/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("", responseContent);
    }

    [Fact, TestPriority(7)]
    public async Task GetInventoryEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    }
}
