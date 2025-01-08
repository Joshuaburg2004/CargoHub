using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers.ControllersV2
{
    [ApiController]
    [Route("/api/v2/warehouses")]
    public class WarehouseControllerV2 : Controller
    {
        private readonly IWarehouseServiceV2 _warehouseservice;

        public WarehouseControllerV2(IWarehouseServiceV2 warehouseservice)
        {
            _warehouseservice = warehouseservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllWarehouses([FromQuery] int? pageIndex)
        {
            var warhouses = await _warehouseservice.GetAllWarehouses(pageIndex);
            return Ok(warhouses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehousesById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
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
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _warehouseservice.LoadFromJson(path);
            return Ok();
        }
    }
}