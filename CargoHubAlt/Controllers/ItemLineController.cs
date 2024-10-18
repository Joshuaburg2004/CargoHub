using Microsoft.AspNetCore.Mvc;


[Route("api/v1/itemLines")]
public class ItemLineController: Controller
{
    readonly IItemLineService _itemsService;
    public ItemLineController(IItemLineService itemsservice)
    {
        this._itemsService = itemsservice;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllItemLines()
    {
        return Ok(await this._itemsService.GetAllItemLine());
    }

    [HttpGet()]
    public async Task<IActionResult> GetOneItemLine([FromQuery] Guid id)
    {
        Item_line? toReturn = await this._itemsService.FindItemLine(id);
        if (toReturn is null) return NotFound($"ID {id} not found");
        else return Ok(toReturn);

    }

    [HttpGet()]
    public async Task<IActionResult> getBatchItemLine([FromQuery] IEnumerable<Guid> ids)
    {
        IEnumerable<Item_line?> found = await this._itemsService.FindManyItemLine(ids);
        return Ok(found);
    }

    [HttpPost()]
    public async Task<IActionResult> AddItemLine([FromBody] Item_line? toAdd)
    {
        if (toAdd is null) return BadRequest("this is not an item Line");
        Guid? success = await this._itemsService.AddItemLine(toAdd);
        if (success is not null) return Ok(success);
        else return BadRequest("something went wrong adding the item");
    }

    [HttpPut()]
    public async Task<IActionResult> putItemLine([FromQuery] Guid id, [FromBody] Item_line toupdateto)
    {
        Item_line? success = await this._itemsService.UpdateItemLine(id, toupdateto);

        if (success is null) return NotFound($"Id not Found: {id}");
        return Ok(success);
    }

    [HttpDelete()]
    public async Task<IActionResult> deleteItemLine([FromQuery] Guid id)
    {
        Item_line? success = await this._itemsService.DeleteItemLine(id);

        if (success is null) return NotFound($"ID not found: {id}");
        else return Ok(success);
    }

}