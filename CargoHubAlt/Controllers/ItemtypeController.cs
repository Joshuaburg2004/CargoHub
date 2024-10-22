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
    public async Task<IActionResult> GetItemLineById([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest("ID is empty");
        return Ok(await _itemsService.GetItemLineById(id));
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddItemLine([FromBody] Item_type? itemtype)
    {
        if (itemtype is null) return BadRequest("this is not an item Line");
        Guid? success = await _itemsService.AddItemType(itemtype);
        if (success is null) return BadRequest("Item Line not added");
        else return Ok(success);
    }

    [HttpPut("update")]
    public async Task<IActionResult> putItemLine([FromQuery] Guid id, [FromBody] Item_type itemtype)
    {
        if (id == Guid.Empty) return BadRequest("ID is empty");
        return Ok(await _itemsService.UpdateItemType(id, itemtype));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> deleteItemLine([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest("ID is empty");
        return Ok(await _itemsService.DeleteItemType(id));
    }

}