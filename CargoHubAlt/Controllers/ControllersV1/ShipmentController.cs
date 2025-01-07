using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV1;
using CargoHubAlt.Models;
using CargoHubAlt.Services.ServicesV1;

namespace CargoHub.Controllers.ControllersV1
{
    [ApiController]
    [Route("api/v1/shipments")]
    public class ShipmentControllerV1 : Controller
    {
        private readonly ILogger<ShipmentControllerV1> _logger;
        private readonly IShipmentServiceV1 _shipmentService;

        public ShipmentControllerV1(IShipmentServiceV1 shipmentService, ILogger<ShipmentControllerV1> logger)
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
                return NotFound();
            }
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var shipment = await _shipmentService.GetShipment(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return Ok(shipment);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsInShipment([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var items = await _shipmentService.GetItemsfromShipmentById(id);
            if (items == null)
            {
                return NotFound();
            }
            if (items.Count == 1) _logger.LogInformation($"Found {items.Count} item in shipment with id:{id}");
            return Ok(items);
        }

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersInShipment([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var orders = await _shipmentService.GetOrdersFromShipmentById(id);
            if (orders == null)
            {
                return NotFound();
            }
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
        {
            if (shipment is null)
            {
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
        [HttpPut("{id}/commit")]
        public async Task<IActionResult> Commit([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await _shipmentService.CommitShipmentById(id);
            return Ok($"Committed the shipment with id:{id}");
        }
    }
}