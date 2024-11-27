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
public class InventoryIntegratieTest : BaseTest
{
    public string requestUri = "/api/v2/inventories";

    private Inventory _TestInventory = new(1, "P000001", "test", "63-OFFTq0T", new List<int>() { 3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817 }, 40, 40, 40, 40, 40);

    private Inventory _TestInventoryPut = new(1, "P000001", "test", "hopeful", new List<int>() { 3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817 }, 60, 60, 60, 60, 60);

    public InventoryIntegratieTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact, TestPriority(0)]
    public async Task GetAllInventoriesEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync(requestUri);
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", result);
    }

    [Fact, TestPriority(1)]
    public async Task GetOneInventoryBeforeAdding()
    {
        var response = await _client.GetAsync($"{requestUri}/1");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(result);
        Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        // It should absolutely be either 400 bad request or 404 not found,but it is 200 OK.
        // This is not intended behavior.
    }

    [Fact, TestPriority(2)]
    public async Task CreateInventory()
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(requestUri, _TestInventory);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task GetInventoryOne()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Inventory? inventoryCompared = await response.Content.ReadFromJsonAsync<Inventory>();
        Xunit.Assert.NotNull(inventoryCompared);

        Xunit.Assert.Equal(_TestInventory.Id, inventoryCompared.Id);
        Xunit.Assert.Equal(_TestInventory.ItemId, inventoryCompared.ItemId);
        Xunit.Assert.Equal(_TestInventory.Description, inventoryCompared.Description);
        Xunit.Assert.Equal(_TestInventory.ItemReference, inventoryCompared.ItemReference);
        Xunit.Assert.Equal(_TestInventory.Locations, inventoryCompared.Locations);
        Xunit.Assert.Equal(_TestInventory.TotalOnHand, inventoryCompared.TotalOnHand);
        Xunit.Assert.Equal(_TestInventory.TotalExpected, inventoryCompared.TotalExpected);
        Xunit.Assert.Equal(_TestInventory.TotalOrdered, inventoryCompared.TotalOrdered);
        Xunit.Assert.Equal(_TestInventory.TotalAllocated, inventoryCompared.TotalAllocated);
        Xunit.Assert.Equal(_TestInventory.TotalAvailable, inventoryCompared.TotalAvailable);
    }

    [Fact, TestPriority(4)]
    public async Task UpdateInventorys()
    {
        HttpResponseMessage response = await _client.PutAsJsonAsync($"{requestUri}/1", _TestInventoryPut);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task GetUpdatedInventoryTest()
    {
        HttpResponseMessage response = await _client.GetAsync($"{requestUri}/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Inventory? inventoryCompared = await response.Content.ReadFromJsonAsync<Inventory>();
        Xunit.Assert.NotNull(inventoryCompared);

        Xunit.Assert.Equal(_TestInventoryPut.Id, inventoryCompared.Id);
        Xunit.Assert.Equal(_TestInventoryPut.ItemId, inventoryCompared.ItemId);
        Xunit.Assert.Equal(_TestInventoryPut.Description, inventoryCompared.Description);
        Xunit.Assert.Equal(_TestInventoryPut.ItemReference, inventoryCompared.ItemReference);
        Xunit.Assert.Equal(_TestInventoryPut.Locations, inventoryCompared.Locations);
        Xunit.Assert.Equal(_TestInventoryPut.TotalOnHand, inventoryCompared.TotalOnHand);
        Xunit.Assert.Equal(_TestInventoryPut.TotalExpected, inventoryCompared.TotalExpected);
        Xunit.Assert.Equal(_TestInventoryPut.TotalOrdered, inventoryCompared.TotalOrdered);
        Xunit.Assert.Equal(_TestInventoryPut.TotalAllocated, inventoryCompared.TotalAllocated);
        Xunit.Assert.Equal(_TestInventoryPut.TotalAvailable, inventoryCompared.TotalAvailable);
    }

    [Fact, TestPriority(6)]
    public async Task DeleteInventory()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"{requestUri}/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(7)]
    public async Task GetOneInventoryAfterDelete()
    {
        var response = await _client.GetAsync($"{requestUri}/1");
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(result);
        Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        // It should absolutely be either 400 bad request or 404 not found,but it is 200 OK.
        // This is not intended behavior.
    }

    [Fact, TestPriority(8)]
    public async Task GetAllInventorysEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync(requestUri);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    }
}
