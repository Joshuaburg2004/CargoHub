using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("/api/v1/warehouses")]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseservice;

        public WarehouseController(IWarehouseService warehouseservice)
        {
            _warehouseservice = warehouseservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warhouses = await _warehouseservice.GetAllWarehouses();
            return Ok(warhouses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehousesById([FromRoute] int id)
        {
            var warehouse = await _warehouseservice.GetWarehouseById(id);
            if (warehouse == null)
                return NotFound();
            return Ok(warehouse);
        }

        [HttpPost()]
        public async Task<IActionResult> AddWarehouse([FromBody] Warehouse newwarehouse)
        {
            if (newwarehouse == null) return BadRequest();
            var warehouse = await _warehouseservice.AddWarehouse(newwarehouse);
            if (warehouse == null) return BadRequest();
            return Created("", "");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse([FromRoute] int id, [FromBody] Warehouse warehouse)
        {
            if (id <= 0 || warehouse == null) return BadRequest();
            var updatedwarehouse = await _warehouseservice.UpdateWarehouse(id, warehouse);
            return Ok(updatedwarehouse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            await _warehouseservice.DeleteWarehouse(id);
            return Ok();
        }

        [HttpGet("{id}/locations")]
        public async Task<IActionResult> GetLocationsByWarehouse([FromRoute] int id)
        {
            var locations = await _warehouseservice.GetLocationsfromWarehouseById(id);
            return Ok(locations);
        }
    }
}