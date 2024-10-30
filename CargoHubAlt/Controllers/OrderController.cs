using Microsoft.AspNetCore.Mvc;

[Route("api/v1/orders")]
public class OrderController : Controller
{
    readonly IOrderService _orderservice;
    public OrderController(IOrderService orderService)
    {
        _orderservice = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        List<Order>? orders = await _orderservice.GetOrders();
        if (orders == null)
        {
            return NotFound();
        }
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        Order? order = await _orderservice.GetOrder(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetOrderedItems([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        List<OrderedItem>? items = await _orderservice.GetOrderedItems(id);
        if (items == null)
        {
            return NotFound();
        }
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        if (order == null)
        {
            return BadRequest();
        }
        else if (!await _orderservice.AddOrder(order))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
    {
        if (id <= 0 || order == null)
        {
            return BadRequest();
        }
        else if (!await _orderservice.UpdateOrder(order))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id}/items")]
    public async Task<IActionResult> UpdateOrderedItems([FromRoute] int id, [FromBody] List<OrderedItem> items)
    {
        if (id <= 0 || items == null)
        {
            return BadRequest();
        }
        else if (!await _orderservice.UpdateOrderedItems(id, items))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveOrder([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        else if (!await _orderservice.RemoveOrder(id))
        {
            return BadRequest();
        }
        return Ok();
    }
}