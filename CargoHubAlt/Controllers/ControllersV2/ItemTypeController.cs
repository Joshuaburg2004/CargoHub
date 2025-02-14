using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHub.Controllers.ControllersV2
{
    [ApiController]
    [Route("api/v2/item_types")]
    public class ItemTypeControllerV2 : Controller
    {
        readonly IItemTypeServiceV2 _itemsService;
        public ItemTypeControllerV2(IItemTypeServiceV2 itemsservice)
        {
            _itemsService = itemsservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllItemTypes([FromQuery] int? pageIndex)
        {
            return Ok(await _itemsService.GetAllItemType(pageIndex));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemTypeById([FromRoute] int id)
        {
            if (id < 0) return BadRequest("invalid ID");
            var item_type = await _itemsService.GetItemTypeById(id);


            // accoring to the original test
            // if (item_type is null) return Ok("null");
            // return Ok(item_type);


            if (item_type is null) return NotFound("Item Type not found");
            else return Ok(item_type);
        }


        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsfromItemTypeById([FromRoute] int id)
        {
            if (id < 0) return BadRequest("invalid ID");
            IEnumerable<Item>? items = await _itemsService.GetItemsfromItemTypeById(id);
            if (items is null || items.Count() == 0) return NotFound($"Item Type with ID: {id} not found");
            else return Ok(items);
        }

        [HttpPost()]
        public async Task<IActionResult> AddItemType([FromBody] ItemType? itemtype)
        {
            if (itemtype is null) return BadRequest("this is not an item Type");
            int? success = await _itemsService.AddItemType(itemtype);
            if (success is null) return BadRequest("Item Type not added");
            else return Created("api/v1/item_types", itemtype.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemType([FromRoute] int id, [FromBody] ItemType itemtype)
        {
            if (id < 0) return BadRequest("invalid ID");

            var success = await _itemsService.UpdateItemType(id, itemtype);
            if (success is not null) return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType([FromRoute] int id)
        {
            if (id < 0) return BadRequest("invalid ID");
            var success = await _itemsService.DeleteItemType(id);

            if (success is not null) return Ok();
            else return BadRequest("id not found");
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadLocations([FromRoute] string path)
        {
            await _itemsService.LoadFromJson(path);
            return Ok();
        }
    }
}
