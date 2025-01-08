using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV1;
using CargoHubAlt.Models;

namespace CargoHub.Controllers.ControllersV1
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderControllerV1 : Controller
    {
        private readonly ILogger<OrderControllerV1> _logger;
        private readonly IOrderServiceV1 _orderservice;
        public OrderControllerV1(IOrderServiceV1 orderService, ILogger<OrderControllerV1> logger)
        {
            _orderservice = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            List<Order>? orders = await _orderservice.GetOrders();
            if (orders == null || orders.Count == 0)
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
            var apiKey = Request.Headers["api_key"];
            if (order == null)
            {
                return BadRequest("Order is null");
            }
            else if (!await _orderservice.AddOrder(order))
            {
                return NotFound("Order not added");
            }
            _logger.LogInformation($"Order with id: {order.Id} added, Api_key: {apiKey}");
            return Created("", "");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            var apiKey = Request.Headers["api_key"];
            if (id <= 0 || order == null)
            {
                return BadRequest();
            }
            var result = await _orderservice.UpdateOrder(order);
            if (result == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Order with id: {id} updated, changed fields: {result}, Api_key: {apiKey}");
            return Ok();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateOrderedItems([FromRoute] int id, [FromBody] List<OrderedItem> items)
        {
            var apiKey = Request.Headers["api_key"];
            if (id <= 0 || items == null)
            {
                return BadRequest();
            }
            string ChangedFields = await _orderservice.UpdateOrderedItems(id, items);
            if (ChangedFields == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Items for order with id: {id} updated, changed fields: {ChangedFields}, Api_key: {apiKey}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder([FromRoute] int id)
        {
            var apiKey = Request.Headers["api_key"];
            if (id <= 0)
            {
                return BadRequest();
            }
            else if (!await _orderservice.RemoveOrder(id))
            {
                return NotFound();
            }
            _logger.LogInformation($"Order with id: {id} removed, Api_key: {apiKey}");
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