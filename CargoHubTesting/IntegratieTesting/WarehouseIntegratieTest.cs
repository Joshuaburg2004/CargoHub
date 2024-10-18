using System.Net;
using System.Text.Json;
using CargoHubAlt.Models;
using Xunit;

namespace MyTests;

[TestCaseOrderer("MyTests.PriorityOrderer", "CargoHubTesting")]
public class WarehouseIntratieTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    public static Guid WarehouseId = Guid.NewGuid();
    public WarehouseIntratieTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact, TestPriority(0)]
    public async Task TestGetAllWarehousesEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/warehouses");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
    }

    // Inseert TestCreateWarehouse here:
    // ...

    [Fact, TestPriority(1)]
    public async Task TestGetAllWarehouses()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/warehouses");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.NotEqual("[]", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(2)]
    public async Task TestGetWarehousesById()
    {
        // TODO: Create TestCreateWarehouse(), so WareHouseId can be filled with an Id, which will be used in this testcase
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/warehouses/{WarehouseId}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var warehouse = JsonSerializer.Deserialize<Warehouse>(content, options);

        Xunit.Assert.NotNull(warehouse);
        Xunit.Assert.Equal(WarehouseId, warehouse.Id);
    }
}
