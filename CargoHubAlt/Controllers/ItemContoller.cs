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
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] string[] ids)
    {
        Item[] items = await itemsService.GetItems(ids);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem([FromRoute] string id)
    {
        return Ok();
    }
}