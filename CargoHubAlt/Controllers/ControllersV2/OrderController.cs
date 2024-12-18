using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHub.Controllers.ControllersV2
{
    [ApiController]
    [Route("api/v2/orders")]
    public class OrderControllerV2 : Controller
    {
        private readonly ILogger<OrderControllerV2> _logger;
        private readonly IOrderServiceV2 _orderservice;
        public OrderControllerV2(IOrderServiceV2 orderService, ILogger<OrderControllerV2> logger)
        {
            _orderservice = orderService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrders()
        {
            List<Order>? orders = await _orderservice.GetOrders();
            if (orders == null || orders.Count == 0)
            {
                _logger.LogInformation("No orders found");
                return NotFound();
            }
            _logger.LogInformation($"Found {orders.Count} orders");
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            Order? order = await _orderservice.GetOrder(id);
            if (order == null)
            {
                _logger.LogInformation($"No order found with id: {id}");
                return NotFound();
            }
            _logger.LogInformation($"Order found with id: {id}");
            return Ok(order);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetIncoming()
        {
            IEnumerable<Order>? ToReturn = await this._orderservice.GetPendingOrders();
            if (ToReturn is null) return NotFound("No undelivered Orders found");
            return Ok(ToReturn);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetOrderedItems([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            List<OrderedItem>? items = await _orderservice.GetOrderedItems(id);
            if (items == null)
            {
                _logger.LogInformation($"No items found for order with id: {id}");
                return NotFound();
            }
            _logger.LogInformation($"Items found for order with id: {id}");
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            if (order == null)
            {
                _logger.LogInformation("Order is null");
                return BadRequest("Order is null");
            }
            else if (!await _orderservice.AddOrder(order))
            {
                _logger.LogInformation("Order not added");
                return NotFound("Order not added");
            }
            _logger.LogInformation($"Order with id: {order.Id} added");
            return Created("", "");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (id <= 0 || order == null)
            {
                _logger.LogInformation("Invalid id or order");
                return BadRequest();
            }
            else if (!await _orderservice.UpdateOrder(order))
            {
                _logger.LogInformation("Order not updated");
                return NotFound();
            }
            _logger.LogInformation($"Order with id: {id} updated");
            return Ok();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateOrderedItems([FromRoute] int id, [FromBody] List<OrderedItem> items)
        {
            if (id <= 0 || items == null)
            {
                _logger.LogInformation("Invalid id or items");
                return BadRequest();
            }
            else if (!await _orderservice.UpdateOrderedItems(id, items))
            {
                _logger.LogInformation("Items not updated");
                return NotFound();
            }
            _logger.LogInformation($"Items for order with id: {id} updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            else if (!await _orderservice.RemoveOrder(id))
            {
                _logger.LogInformation("Order not removed");
                return NotFound();
            }
            _logger.LogInformation($"Order with id: {id} removed");
            return Ok();
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _orderservice.LoadFromJson(path);
            _logger.LogInformation($"Orders loaded from json path: {path}");
            return Ok();
        }
    }
}