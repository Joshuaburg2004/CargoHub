using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("api/v1/items")]
    public class ItemController : Controller
    {
        readonly IItemsService _itemsService;
        public ItemController(IItemsService itemsservice)
        {
            _itemsService = itemsservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            IEnumerable<Item> found = await this._itemsService.GetItems();
            return Ok(found);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            Item? found = await this._itemsService.GetItem(id);
            if (found is null) return NotFound($"Item with UID {id} not found");
            else return Ok(found);
        }


        [HttpPost()]
        public async Task<IActionResult> AddItem([FromBody] Item? item)
        {
            if (item is null) return BadRequest("This is not an item");
            string? toReturn = await this._itemsService.AddItem(item);

            if (toReturn is null) return BadRequest("Failed to add item");
            if (toReturn == "Existed") return BadRequest($"Item with Uid {item.Uid} already exists");
            else return Created("api/v1/items", toReturn);
        }

        [HttpDelete("{toRemove}")]
        public async Task<IActionResult> RemoveItem([FromRoute] string toRemove)
        {
            Item? toReturn = await this._itemsService.RemoveItem(toRemove);

            if (toReturn is null) return NotFound($"Item with UID {toRemove} not found");
            else return Ok(toReturn);
        }

        [HttpPut("{toUpdate}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string toUpdate, [FromBody] Item UpdateTo)
        {
            Item? toReturn = await this._itemsService.UpdateItem(toUpdate, UpdateTo);

            if (toReturn is null) return NotFound($"Item with UID {toUpdate} not found");
            else return Ok(toReturn);
        }
        [HttpGet("{id}/inventory")]
        public async Task<IActionResult> GetInventoryByItem([FromRoute] string id)
        {
            IEnumerable<Inventory> found = await this._itemsService.GetInventoryByItem(id);

            if (found is null || found.Count() == 0) return NotFound($"Inventory for item with UID {id} not found");
            else return Ok(found);
        }
        [HttpGet("{id}/inventory/totals")]
        public async Task<IActionResult> GetInventoryTotalsByItem([FromRoute] string id)
        {
            Dictionary<string, int> found = await this._itemsService.GetInventoryTotalsByItem(id);
            return Ok(found);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path){
            await _itemsService.LoadFromJson(path);
            return Ok();
        }
    }
}
