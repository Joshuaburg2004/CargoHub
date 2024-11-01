using Microsoft.AspNetCore.Mvc;


[Route("api/v1/item_types")]
public class ItemTypeController : Controller
{
    readonly IItemTypeService _itemsService;
    public ItemTypeController(IItemTypeService itemsservice)
    {
        _itemsService = itemsservice;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllItemTypes()
    {
        return Ok(await _itemsService.GetAllItemType());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemTypeById([FromRoute] int id)
    {
        if ( id < 0) return BadRequest("invalid ID");
        var item_type = await _itemsService.GetItemTypeById(id);
        if (item_type is null) return NotFound("Item Type not found");
        else return Ok(item_type);
    }

    [HttpPost()]
    public async Task<IActionResult> AddItemType([FromBody] Item_type? itemtype)
    {
        if (itemtype is null) return BadRequest("this is not an item Type");
        int? success = await _itemsService.AddItemType(itemtype);
        if (success is null) return BadRequest("Item Type not added");
        else return Ok(success);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> putItemType([FromRoute] int id, [FromBody] Item_type itemtype)
    {
        if ( id < 0) return BadRequest("invalid ID");
        return Ok(await _itemsService.UpdateItemType(id, itemtype));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteItemType([FromRoute] int id)
    {
        if ( id < 0) return BadRequest("invalid ID");
        return Ok(await _itemsService.DeleteItemType(id));
    }

}