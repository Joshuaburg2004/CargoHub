using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

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
            ItemGroup? toReturn = await this._itemsService.FindItemGroup(id);

            // if (toReturn is null) return Ok("null");
            // return Ok(toReturn);

            if (toReturn is null) return NotFound($"ID {id} not found");
            else return Ok(toReturn);

        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsfromItemGroupById([FromRoute] int id)
        {
            if (id < 0) return BadRequest("invalid id");
            var success = await this._itemsService.GetItemsfromItemGroupById(id);
            if (success is null || success.Count() == 0) return NotFound($"Item Group with ID: {id} not found");
            else return Ok(success);
        }


        [HttpPost()]
        public async Task<IActionResult> AddItemGroup([FromBody] ItemGroup? toAdd)
        {
            if (toAdd is null) return BadRequest("this is not an item group");
            int? success = await this._itemsService.AddItemGroup(toAdd);
            if (success is not null) return Created("api/v1/item_groups", toAdd.Id);
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemGroup([FromRoute] int id, [FromBody] ItemGroup toupdateto)
        {

            ItemGroup? success = await this._itemsService.UpdateItemGroup(id, toupdateto);

            // return Ok();

            if (success is null) return NotFound($"Id not Found: {id}");
            return Ok(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemgroup([FromRoute] int id)
        {

            ItemGroup? success = await this._itemsService.DeleteItemGroup(id);

            // return Ok();

            if (success is null) return NotFound($"ID not found: {id}");
            else return Ok(success);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadLocations([FromRoute] string path){
            await _itemsService.LoadFromJson(path);
            return Ok();
        }
    }
}
