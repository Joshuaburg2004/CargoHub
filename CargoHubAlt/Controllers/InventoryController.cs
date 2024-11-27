using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("api/v1/Inventories")]
    public class InventoryController : Controller
    {
        readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventorysservice)
        {
            this._inventoryService = inventorysservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllInventories()
        {
            return Ok(await this._inventoryService.GetAllInventories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneInventory([FromRoute] int id)
        {
            Inventory? toReturn = await this._inventoryService.GetOneInventory(id);
            if (toReturn is null) return NotFound($"ID {id} not found");
            else return Ok(toReturn);

        }

        [HttpPost()]
        public async Task<IActionResult> AddInventory([FromBody] Inventory? toAdd)
        {
            if (toAdd is null) return BadRequest("This is not an inventory");
            int? success = await this._inventoryService.CreateInventory(toAdd);
            if(success is null) return BadRequest("This is not an inventory");
            if(success == -1) return BadRequest("This inventory already exists");
            return Created("api/v1/inventories", success);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory([FromRoute] int id, [FromBody] Inventory? toupdateto)
        {
            if (toupdateto is null) return BadRequest("This is not an inventory");
            Inventory? success = await this._inventoryService.UpdateInventory(id, toupdateto);
            // return Ok("");


            if (success is null) return NotFound($"Id not Found: {id}");
            return Ok(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory([FromRoute] int id)
        {
            Inventory? success = await this._inventoryService.DeleteInventory(id);

            // return Ok("");

            if (success is null) return NotFound($"ID not found: {id}");
            else return Ok(success);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadLocations([FromRoute] string path){
            await _inventoryService.LoadFromJson(path);
            return Ok();
        }
    }
}
