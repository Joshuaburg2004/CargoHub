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
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
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
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
        {
            if (shipment is null) return BadRequest();
            await _shipmentService.AddShipment(shipment);
            return Created("Created location", shipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment([FromRoute] int id, [FromBody] Shipment shipment)
        {
            if (id <= 0 || shipment is null) return BadRequest();
            await _shipmentService.UpdateShipment(id, shipment);
            return Ok();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateItemsInShipment([FromRoute] int id, [FromBody] List<ShipmentItem> items)
        {
            if (id <= 0 || items == null) return BadRequest();
            await _shipmentService.Update_items_in_Shipment(id, items);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            await _shipmentService.DeleteShipment(id);
            return Ok();
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path){
            await _shipmentService.LoadFromJson(path);
            return Ok();
        }
    }
}