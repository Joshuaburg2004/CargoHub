using CargoHubAlt.Models;
using Microsoft.AspNetCore.Mvc;

[Route("/api/v1/warehouses")]
public class WarehouseController : Controller
{
    private readonly IWarehouse _warehouseservice;

    public WarehouseController(IWarehouse warehouseservice)
    {
        _warehouseservice = warehouseservice;
    }

    [HttpGet]
    public async Task<IActionResult> GetWarehouses()
    {
        var warhouses = await _warehouseservice.GetWarehouses();
        return Ok(warhouses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWarehousesById([FromRoute] Guid id)
    {
        var warehouse = await _warehouseservice.GetWarehousesById(id);
        return Ok(warehouse);
    }

    [HttpPost]
    public async Task<IActionResult> AddWarehouse([FromBody] Warehouse warehouse)
    {
        if (warehouse == null) return BadRequest();
        await _warehouseservice.AddWarehouse(warehouse);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWarehouse([FromBody] Warehouse warehouse)
    {
        if (warehouse == null) return BadRequest();
        await _warehouseservice.UpdateWarehouse(warehouse);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouse([FromRoute] Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        await _warehouseservice.DeleteWarehouse(id);
        return Ok();
    }
}