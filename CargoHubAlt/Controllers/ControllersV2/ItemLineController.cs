using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers.ControllersV2
{
    [ApiController]
    [Route("/api/v2/item_lines")]
    public class ItemLineControllerV2 : Controller
    {
        readonly IItemLineServiceV2 _itemsService;
        public ItemLineControllerV2(IItemLineServiceV2 itemsservice)
        {
            this._itemsService = itemsservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllItemLines()
        {
            return Ok(await this._itemsService.GetAllItemLine());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneItemLine([FromRoute] int id)
        {
            ItemLine? toReturn = await this._itemsService.FindItemLine(id);

            // if (toReturn is null) return Ok("null");
            // else return Ok(toReturn);

            if (toReturn is null) return NotFound($"ID {id} not found");
            else return Ok(toReturn);

        }

        [HttpGet("batch")]
        public async Task<IActionResult> getBatchItemLine([FromQuery] IEnumerable<int> ids)
        {
            IEnumerable<ItemLine?> found = await this._itemsService.FindManyItemLine(ids);
            return Ok(found);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsfromItemLineById([FromRoute] int id)
        {
            if (id < 0) return BadRequest("invalid id");
            var success = await this._itemsService.GetItemsfromItemLineById(id);
            if (success is null || success.Count() == 0) return NotFound($"Item_line with ID: {id} not found");
            else return Ok(success);
        }


        [HttpPost()]
        public async Task<IActionResult> AddItemLine([FromBody] ItemLine? toAdd)
        {
            if (toAdd is null) return BadRequest("this is not an item Line");
            int? success = await this._itemsService.AddItemLine(toAdd);
            if (success is not null) return Created("api/v2/item_lines", toAdd.Id);
            else return BadRequest("something went wrong adding the item");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemLine([FromRoute] int id, [FromBody] ItemLine toupdateto)
        {
            await _itemsService.UpdateItemLine(id, toupdateto);
            return Ok("");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemLine([FromRoute] int id)
        {
            ItemLine? success = await this._itemsService.DeleteItemLine(id);

            if (success is null) return NotFound($"ID not found: {id}");
            else return Ok();
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadLocations([FromRoute] string path)
        {
            await _itemsService.LoadFromJson(path);
            return Ok();
        }
    }
}
