using Microsoft.AspNetCore.Mvc;

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
        var shipments = await _shipmentService.GetShipments();
        if (shipments == null)
        {
            return BadRequest();
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
        var shipment = await _shipmentService.GetShipment(id);
        if (shipment == null)
        {
            return NotFound();
        }
        return Ok(shipment.Items);
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetOrdersInShipment([FromRoute] int id)
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
        Order? order = await _shipmentService.GetOrderInShipment(id);
        if (order == null)
        {
            return NotFound("order not found");
        }
        return Ok(order);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShipment([FromRoute] int id, [FromBody] Shipment shipment)
    {
        if (shipment == null)
        {
            return BadRequest();
        }
        else if (!await _shipmentService.UpdateShipment(id, shipment))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id}/items")]
    public async Task<IActionResult> UpdateItemsInShipment([FromRoute] int id, [FromBody] List<ShipmentItem> items)
    {
        if (items == null)
        {
            return BadRequest();
        }
        else if (id <= 0)
        {
            return BadRequest();
        }
        else if (!await _shipmentService.Update_items_in_Shipment(id, items))
        {
            return BadRequest();
        }
        return Ok(items);
    }

    [HttpPut("{id}/orders")]
    public async Task<IActionResult> UpdateOrderInShipment([FromRoute] int id, [FromBody] Order order)
    {
        if (order == null)
        {
            return BadRequest("null order");
        }
        else if (id <= 0)
        {
            return BadRequest("id <= 0");
        }
        else if (!await _shipmentService.Update_Order_in_Shipment(id, order))
        {
            return BadRequest("update order failed");
        }
        return Ok(order);
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