using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers.ControllersV2
{
    [ApiController]
    [Route("api/v2/items")]
    public class ItemControllerV2 : Controller
    {
        private readonly ILogger<ItemControllerV2> _logger;
        private readonly IItemsServiceV2 _itemsService;
        public ItemControllerV2(IItemsServiceV2 itemsservice, ILogger<ItemControllerV2> logger)
        {
            _itemsService = itemsservice;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems([FromQuery] int? pageIndex)
        {
            List<Item> found = await this._itemsService.GetItems(pageIndex);
            _logger.LogInformation($"Found {found.Count} Items");
            return Ok(found);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            Item? found = await this._itemsService.GetItem(id);
            if (found is null)
            {
                _logger.LogInformation($"Item with UID {id} not found");
                return NotFound($"Item with UID {id} not found");
            }
            _logger.LogInformation($"Item with UID {id} found");
            return Ok(found);
        }


        [HttpPost()]
        public async Task<IActionResult> AddItem([FromBody] Item? item)
        {
            if (item is null)
            {
                _logger.LogInformation("Invalid item");
                return BadRequest("This is not an item");
            }
            string? toReturn = await this._itemsService.AddItem(item);

            if (toReturn is null)
            {
                _logger.LogInformation($"Failed to add item with Uid {item.Uid}");
                return BadRequest($"Failed to add item with Uid {item.Uid}");
            }
            if (toReturn == "Existed")
            {
                _logger.LogInformation($"Item with Uid {item.Uid} already exists");
                return BadRequest($"Item with Uid {item.Uid} already exists");
            }
            _logger.LogInformation($"Item with Uid {item.Uid} added");
            return Created("api/v1/items", toReturn);
        }

        [HttpDelete("{toRemove}")]
        public async Task<IActionResult> RemoveItem([FromRoute] string toRemove)
        {
            Item? toReturn = await this._itemsService.RemoveItem(toRemove);

            if (toReturn is null)
            {
                _logger.LogInformation($"Item with UID {toRemove} not found");
                return NotFound($"Item with UID {toRemove} not found");
            }
            _logger.LogInformation($"Item with UID {toRemove} removed");
            return Ok(toReturn);
        }

        [HttpPut("{toUpdate}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string toUpdate, [FromBody] Item UpdateTo)
        {
            Item? toReturn = await this._itemsService.UpdateItem(toUpdate, UpdateTo);

            if (toReturn is null)
            {
                _logger.LogInformation($"Item with UID {toUpdate} not found");
                return NotFound($"Item with UID {toUpdate} not found");
            }
            _logger.LogInformation($"Item with UID {toUpdate} updated");
            return Ok(toReturn);
        }
        [HttpGet("{id}/inventory")]
        public async Task<IActionResult> GetInventoryByItem([FromRoute] string id)
        {
            IEnumerable<Inventory> found = await this._itemsService.GetInventoryByItem(id);

            if (found is null || found.Count() == 0)
            {
                _logger.LogInformation($"Inventory for item with UID {id} not found");
                return NotFound($"Inventory for item with UID {id} not found");
            }
            _logger.LogInformation($"Inventory for item with UID {id} found");
            return Ok(found);
        }
        [HttpGet("{id}/inventory/totals")]
        public async Task<IActionResult> GetInventoryTotalsByItem([FromRoute] string id)
        {
            Dictionary<string, int> found = await this._itemsService.GetInventoryTotalsByItem(id);
            _logger.LogInformation($"Inventory totals for item with UID {id} found");
            return Ok(found);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _itemsService.LoadFromJson(path);
            return Ok();
        }
    }
}
