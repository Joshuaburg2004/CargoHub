using System.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("api/v1/items")]
    public class ItemController : Controller
    {
        readonly IItemsService itemsService;
        public ItemController(IItemsService itemsservice)
        {
            itemsService = itemsservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            IEnumerable<Item> found = await this.itemsService.GetItems();
            return Ok(found);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            Item? found = await this.itemsService.GetItem(id);
            if (found is null) return NotFound($"Item with UID {id} not found");
            else return Ok(found);
        }


        [HttpPost()]
        public async Task<IActionResult> AddItem([FromBody] Item? item)
        {
            if (item is null) return BadRequest("Item is null");
            string? toReturn = await this.itemsService.AddItem(item);

            return Created("", "");

            // if (toReturn is null) return BadRequest("Failed to add item");
            // else return Ok(toReturn);
        }

        [HttpDelete("{toRemove}")]
        public async Task<IActionResult> RemoveItem([FromRoute] string toRemove)
        {
            Item? toReturn = await this.itemsService.RemoveItem(toRemove);

            return Ok("");

            // if (toReturn is null) return NotFound($"Item with UID {toRemove} not found");
            // else return Ok(toReturn);
        }

        [HttpPut("{toUpdate}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string toUpdate, [FromBody] Item UpdateTo)
        {
            Item? toReturn = await this.itemsService.UpdateItem(toUpdate, UpdateTo);

            return Ok("");

            // if (toReturn is null) return NotFound($"Item with UID {toUpdate} not found");
            // else return Ok(toReturn);
        }
        [HttpGet("{id}/inventory")]
        public async Task<IActionResult> GetInventoryByItem([FromRoute] string id)
        {
            IEnumerable<Inventory> found = await this.itemsService.GetInventoryByItem(id);

            return Ok(found);

            // if (found is null) return NotFound($"Inventory for item with UID {id} not found");
            // else return Ok(found);
        }
        [HttpGet("{id}/inventory/totals")]
        public async Task<IActionResult> GetInventoryTotalsByItem([FromRoute] string id)
        {
            Dictionary<string, int> found = await this.itemsService.GetInventoryTotalsByItem(id);
            return Ok(found);
        }
    }
}
