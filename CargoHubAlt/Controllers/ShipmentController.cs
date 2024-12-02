using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;
using CargoHubAlt.Services;

namespace CargoHub.Controllers
{
    [ApiController]
    [Route("api/v1/shipments")]
    public class ShipmentController : Controller
    {
        private readonly ILogger<ShipmentController> _logger;
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService, ILogger<ShipmentController> logger)
        {
            _shipmentService = shipmentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetShipments()
        {
            var shipments = await _shipmentService.GetAllShipments();
            if (shipments == null)
            {
                _logger.LogInformation("No shipments found");
                return NotFound();
            }
            _logger.LogInformation($"Found {shipments.Count} shipments");
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            var shipment = await _shipmentService.GetShipment(id);
            if (shipment == null)
            {
                _logger.LogInformation($"Shipment with id:{id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Shipment with id:{id} found");
            return Ok(shipment);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsInShipment([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            var items = await _shipmentService.GetItemsfromShipmentById(id);
            if (items == null)
            {
                _logger.LogInformation($"No items found in shipment with id:{id}");
                return NotFound();
            }
            if (items.Count == 1) _logger.LogInformation($"Found {items.Count} item in shipment with id:{id}");
            _logger.LogInformation($"Found {items.Count} items in shipment with id:{id}");
            return Ok(items);
        }

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersInShipment([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            var orders = await _shipmentService.GetOrdersFromShipmentById(id);
            if (orders == null)
            {
                _logger.LogInformation($"No orders found in shipment with id:{id}");
                return NotFound();
            }
            if (orders.Count == 0)
            {
                _logger.LogInformation($"No orders found in shipment with id:{id}");
                return NotFound();
            }
            _logger.LogInformation($"Found {orders.Count} orders in shipment with id:{id}");
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
        {
            if (shipment is null)
            {
                _logger.LogInformation("Invalid shipment");
                return BadRequest();
            }
            await _shipmentService.AddShipment(shipment);
            _logger.LogInformation($"Created new shipment with id:{shipment.Id}");
            return Created("Created Shipment", shipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment([FromRoute] int id, [FromBody] Shipment shipment)
        {
            if (id <= 0 || shipment is null)
            {
                _logger.LogInformation("Invalid id or shipment");
                return BadRequest();
            }
            await _shipmentService.UpdateShipment(id, shipment);
            _logger.LogInformation($"Updated shipment with id:{id}");
            return Ok();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateItemsInShipment([FromRoute] int id, [FromBody] List<ShipmentItem> items)
        {
            if (id <= 0 || items == null)
            {
                _logger.LogInformation("Invalid id or items");
                return BadRequest();
            }
            await _shipmentService.UpdateItemsInShipment(id, items);
            _logger.LogInformation($"Updated items in shipment with id:{id}");
            return Ok();
        }

        [HttpPut("{id}/orders")]
        public async Task<IActionResult> UpdateOrdersInShipment([FromRoute] int id, [FromBody] List<int> orders)
        {
            if (id <= 0 || orders == null)
            {
                _logger.LogInformation("Invalid id or orders");
                return BadRequest();
            }
            await _shipmentService.UpdateOrdersInShipment(id, orders);
            _logger.LogInformation($"Updated orders in shipment with id:{id}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            await _shipmentService.DeleteShipment(id);
            _logger.LogInformation($"Deleted shipment with id:{id}");
            return Ok();
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _shipmentService.LoadFromJson(path);
            return Ok();
        }
    }
}