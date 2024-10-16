using Microsoft.AspNetCore.Mvc;


[Route("api/v2/itemGroups")]
public class ItemGroupController: Controller
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

    [HttpGet()]
    public async Task<IActionResult> GetOneItemGroup(int id)
    {
        if (id < 0) return BadRequest("Invalid ID!");
        else
        {
            Item_group? toReturn = await this._itemsService.FindItemGroup(id);
            if (toReturn is null) return NotFound($"ID {id} not found");
            else return Ok(toReturn);
        }
    }

    [HttpGet()]
    public async Task<IActionResult> getBatchItemGroup(IEnumerable<int> ids)
    {
        IEnumerable<Item_group?> found = await this._itemsService.FindManyItemGroup(ids);
        return Ok(found);
    }

    [HttpPost()]
    public async Task<IActionResult> AddItemGroup(Item_group? toAdd)
    {
        if (toAdd is null) return BadRequest("this is not an item group");
        bool success = await this._itemsService.AddItemGroup(toAdd);
        if (success) return Ok();
        else return BadRequest();
    }

    [HttpPut()]
    public async Task<IActionResult> putItemGroup(int id, Item_group toupdateto)
    {
        if (id < 0) return BadRequest($"invalid id: {id}");

        Item_group? success = await this._itemsService.UpdateItemGroup(id, toupdateto);

        if (success is null) return NotFound($"Id not Found: {id}");
        return Ok(success);
    }

    [HttpDelete()]
    public async Task<IActionResult> deleteItemgroup(int id)
    {
        if (id < 0) return BadRequest($"invalid id: {id}");

        Item_group? success = await this._itemsService.DeleteItemGroup(id);

        if (success is null) return NotFound($"ID not found: {id}");
        else return Ok(success);
    }

}