using Microsoft.AspNetCore.Mvc;

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
        var shipments = await _shipmentService.GetShipments();
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
        if (shipment == null)
        {
            return BadRequest();
        }
        else if (!await _shipmentService.AddShipment(shipment))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateShipment([FromBody] Shipment shipment)
    {
        if (shipment == null)
        {
            return BadRequest();
        }
        else if (!await _shipmentService.UpdateShipment(shipment))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id}/items")]
    public async Task<IActionResult> UpdateItemsInShipment([FromRoute] int id, int shipmentid, [FromBody] List<ShipmentItem> items)
    {
        if (items == null)
        {
            return BadRequest();
        }
        else if (id <= 0)
        {
            return BadRequest();
        }
        else if (shipmentid <= 0)
        {
            return BadRequest();
        }
        else if (!await _shipmentService.Update_items_in_Shipment(id, shipmentid, items))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipment([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        else if (!await _shipmentService.DeleteShipment(id))
        {
            return BadRequest();
        }
        return Ok();
    }
}