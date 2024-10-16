using CargoHubAlt.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/warehouse")]
public class WarehouseController : Controller
{
    private readonly IWarehouse _warehouseservice;

    public WarehouseController(IWarehouse warehouseservice)
    {
        _warehouseservice = warehouseservice;
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetWarehouses()
    {
        var warhouses = await _warehouseservice.GetWarehouses();
        return Ok(warhouses);
    }

    [HttpGet("getbyid")]
    public async Task<ActionResult> GetWarehousesById(Guid id)
    {
        var warehouse = await _warehouseservice.GetWarehousesById(id);
        return Ok(warehouse);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddWarehouse([FromBody] Warehouse warehouse)
    {
        if (warehouse == null) return BadRequest();
        await _warehouseservice.AddWarehouse(warehouse);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateWarehouse([FromBody] Warehouse warehouse)
    {
        if (warehouse == null) return BadRequest();
        await _warehouseservice.UpdateWarehouse(warehouse);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteWarehouse(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        await _warehouseservice.DeleteWarehouse(id);
        return Ok();
    }
}