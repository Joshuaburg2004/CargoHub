using Microsoft.AspNetCore.Mvc;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("api/v1/item_groups")]
    public class ItemGroupController : Controller
    {
        readonly IItemGroupService _itemsService;
        public ItemGroupController(IItemGroupService itemsservice)
        {
            this._itemsService = itemsservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllItemGroups()
        {
            return Ok(await this._itemsService.GetAllItemGroup());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneItemGroup([FromRoute] int id)
        {
            Item_group? toReturn = await this._itemsService.FindItemGroup(id);

            if (toReturn is null) return Ok("null");
            return Ok(toReturn);

            // if (toReturn is null) return NotFound($"ID {id} not found");
            // else return Ok(toReturn);

        }

        [HttpGet("batch")]
        public async Task<IActionResult> getBatchItemGroup([FromQuery] int[] ids)
        {
            IEnumerable<Item_group?> found = await this._itemsService.FindManyItemGroup(ids);
            return Ok(found);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsfromItemGroupById([FromRoute] int id)
        {
            if (id < 0) return BadRequest("invalid id");
            var success = await this._itemsService.GetItemsfromItemGroupById(id);
            if (success is null) return Ok("null");
            else return Ok(success);

        }


        [HttpPost()]
        public async Task<IActionResult> AddItemGroup([FromBody] Item_group? toAdd)
        {
            if (toAdd is null) return BadRequest("this is not an item group");
            int? success = await this._itemsService.AddItemGroup(toAdd);
            if (success is not null) return Ok(toAdd.Id);
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> putItemGroup([FromRoute] int id, [FromBody] Item_group toupdateto)
        {

            Item_group? success = await this._itemsService.UpdateItemGroup(id, toupdateto);

            return Ok();

            // if (success is null) return NotFound($"Id not Found: {id}");
            // return Ok(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteItemgroup([FromRoute] int id)
        {

            Item_group? success = await this._itemsService.DeleteItemGroup(id);

            return Ok();

            // if (success is null) return NotFound($"ID not found: {id}");
            // else return Ok(success);
        }
    }
}
