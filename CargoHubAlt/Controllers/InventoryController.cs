using Microsoft.AspNetCore.Mvc;


[Route("api/v1/Inventory")]
public class InventoryController : Controller
{
    readonly IInventoryService _inventoryService;
    public InventoryController(IInventoryService inventorysservice)
    {
        this._inventoryService = inventorysservice;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllInventories()
    {
        return Ok(await this._inventoryService.GetAllInventories());
    }

    [HttpGet()]
    public async Task<IActionResult> GetOneInventory([FromQuery] Guid id)
    {
        Inventory? toReturn = await this._inventoryService.FindInventory(id);
        if (toReturn is null) return NotFound($"ID {id} not found");
        else return Ok(toReturn);

    }

    [HttpGet()]
    public async Task<IActionResult> getBatchInventory([FromQuery] Guid[] ids)
    {
        IEnumerable<Inventory?> found = await this._inventoryService.FindManyInventories(ids);
        return Ok(found);
    }

    [HttpPost()]
    public async Task<IActionResult> AddInventory([FromBody] Inventory? toAdd)
    {
        if (toAdd is null) return BadRequest("this is not an inventory");
        Guid? success = await this._inventoryService.CreateInventory(toAdd);
        if (success is not null) return Ok(success);
        else return BadRequest("something went wrong adding the inventory");
    }

    [HttpPut()]
    public async Task<IActionResult> PutInventory([FromQuery] Guid id, [FromBody] Inventory toupdateto)
    {
        Inventory? success = await this._inventoryService.UpdateInventory(id, toupdateto);

        if (success is null) return NotFound($"Id not Found: {id}");
        return Ok(success);
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteInventory([FromQuery] Guid id)
    {
        Inventory? success = await this._inventoryService.DeleteInventory(id);

        if (success is null) return NotFound($"ID not found: {id}");
        else return Ok(success);
    }

}