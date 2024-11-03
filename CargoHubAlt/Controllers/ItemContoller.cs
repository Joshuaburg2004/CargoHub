using System.Numerics;
using Microsoft.AspNetCore.Mvc;

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
        IEnumerable<Item> found = await this.itemsService.GetAllItems();
        return Ok(found);
    }

    [HttpGet("batch")]
    public async Task<IActionResult> GetItemsbatch([FromQuery] string[] ids)
    {
        IEnumerable<Item?> items = await itemsService.GetItemsBatch(ids);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem([FromRoute] string id)
    {
        if (Guid.TryParse(id, out Guid result))
        {
            Item? found = await this.itemsService.GetItem(result);
            if (found is null) return NotFound();
            else return Ok(found);
        }
        return BadRequest();
    }


    [HttpPost()]
    public async Task<IActionResult> AddItem([FromBody] Item? item)
    {
        if (item is null) return BadRequest();
        Guid? toReturn = await this.itemsService.AddItem(item);
        if (toReturn is null) return BadRequest();
        else return Ok(toReturn);
    }

    [HttpDelete("{toRemove}")]
    public async Task<IActionResult> RemoveItem([FromRoute] string toRemove)
    {
        if (Guid.TryParse(toRemove, out Guid found))
        {
            Item? toReturn = await this.itemsService.RemoveItem(found);
            if (toReturn is null) return BadRequest();
            else return Ok(toReturn);
        }
        return BadRequest();
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateItem([FromRoute] string toUpdate, [FromBody] Item UpdateTo)
    {
        if (Guid.TryParse(toUpdate, out Guid found))
        {
            Item? toReturn = await this.itemsService.UpdateItem(found, UpdateTo);
            if (toReturn is null) return BadRequest();
            else return Ok(toReturn);
        }
        return BadRequest();
    }



}