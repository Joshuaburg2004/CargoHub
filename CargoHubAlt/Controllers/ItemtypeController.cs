using Microsoft.AspNetCore.Mvc;


[Route("api/v1/itemtypes")]
public class ItemTypeController : Controller
{
    readonly IItemTypeService _itemsService;
    public ItemTypeController(IItemTypeService itemsservice)
    {
        _itemsService = itemsservice;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllItemTypes()
    {
        return Ok(await _itemsService.GetAllItemType());
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetItemTypeById([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest("ID is empty");
        var item_type = await _itemsService.GetItemTypeById(id);
        if (item_type is null) return NotFound("Item Type not found");
        else return Ok(item_type);
    }

    [HttpPost()]
    public async Task<IActionResult> AddItemType([FromBody] Item_type? itemtype)
    {
        if (itemtype is null) return BadRequest("this is not an item Type");
        Guid? success = await _itemsService.AddItemType(itemtype);
        if (success is null) return BadRequest("Item Type not added");
        else return Ok(success);
    }

    [HttpPut()]
    public async Task<IActionResult> putItemType([FromQuery] Guid id, [FromBody] Item_type itemtype)
    {
        if (id == Guid.Empty) return BadRequest("ID is empty");
        return Ok(await _itemsService.UpdateItemType(id, itemtype));
    }

    [HttpDelete()]
    public async Task<IActionResult> deleteItemType([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest("ID is empty");
        return Ok(await _itemsService.DeleteItemType(id));
    }

}