using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHub.Controllers
{
    [ApiController]
    [Route("api/shipment")]
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

        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
        {
            if (shipment is null) return BadRequest();
            await _shipmentService.AddShipment(shipment);
            return Created("Created location", shipment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShipment([FromRoute] int Shipmentid, [FromBody] Shipment shipment)
        {
            if (Shipmentid <= 0 || shipment is null) return BadRequest();
            await _shipmentService.UpdateShipment(Shipmentid, shipment);
            return Ok();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateItemsInShipment([FromRoute] int id, int shipmentid, [FromBody] List<ShipmentItem> items)
        {
            if (id <= 0 || shipmentid <= 0 || items == null) return BadRequest();
            await _shipmentService.Update_items_in_Shipment(id, shipmentid, items);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            await _shipmentService.DeleteShipment(id);
            return Ok();
        }
    }
}