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

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            IEnumerable<Order>? ToReturn = await this._orderservice.GetPendingOrders();
            if (ToReturn is null || ToReturn.Count() == 0) return NotFound("No undelivered Orders found");
            return Ok(ToReturn);
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
            _logger.LogInformation($"Order with id: {order.Id} added, Apikey: {apiKey}");
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
            string result = await _orderservice.UpdateOrder(order);
            if (result == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Order with id: {id} updated, changed fields: {result}, Apikey: {apiKey}");
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
            _logger.LogInformation($"Items for order with id: {id} updated, changed fields: {ChangedFields}, Apikey: {apiKey}");
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
            _logger.LogInformation($"Order with id: {id} removed, Apikey: {apiKey}");
            return Ok();
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _orderservice.LoadFromJson(path);
            return Ok();
        }
    }
}