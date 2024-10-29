using Microsoft.AspNetCore.Mvc;

[Route("api/v1/orders")]
public class OrderController : Controller
{
    readonly IOrderService _orderservice;
    public OrderController(IOrderService orderService)
    {
        _orderservice = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        if (await _orderservice.AddOrder(order))
            return Ok($"Item with id: {order.Id} added.");
        return BadRequest($"Something went wrong.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveOrder([FromRoute] int id)
    {
        if (await _orderservice.RemoveOrder(id))
            return Ok($"Item with id: {id} deleted.");
        return BadRequest($"Something went wrong.");
    }
}