using Microsoft.AspNetCore.Mvc;


[Route("api/v1/itemGroups")]
public class ItemGroupController: Controller
{
    readonly IItemGroupService _itemsService;
    public ItemGroupController(IItemGroupService itemsservice)
    {
        this._itemsService = itemsservice;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllItemGroups()
    {
        return Ok(await this._itemsService.GetAllItemGroup());
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetOneItemGroup([FromQuery] Guid id)
    {
        Item_group? toReturn = await this._itemsService.FindItemGroup(id);
        if (toReturn is null) return NotFound($"ID {id} not found");
        else return Ok(toReturn);

    }

    [HttpGet("getbatch")]
    public async Task<IActionResult> getBatchItemGroup([FromQuery] Guid[] ids)
    {
        IEnumerable<Item_group?> found = await this._itemsService.FindManyItemGroup(ids);
        return Ok(found);
    }

    [HttpPost()]
    public async Task<IActionResult> AddItemGroup([FromBody] Item_group? toAdd)
    {
        if (toAdd is null) return BadRequest("this is not an item group");
        Guid? success = await this._itemsService.AddItemGroup(toAdd);
        if (success is not null) return Ok(toAdd.Id);
        else return BadRequest();
    }

    [HttpPut()]
    public async Task<IActionResult> putItemGroup([FromQuery] Guid id, [FromBody] Item_group toupdateto)
    {

        Item_group? success = await this._itemsService.UpdateItemGroup(id, toupdateto);

        if (success is null) return NotFound($"Id not Found: {id}");
        return Ok(success);
    }

    [HttpDelete()]
    public async Task<IActionResult> deleteItemgroup([FromQuery] Guid id)
    {

        Item_group? success = await this._itemsService.DeleteItemGroup(id);

        if (success is null) return NotFound($"ID not found: {id}");
        else return Ok(success);
    }

}
