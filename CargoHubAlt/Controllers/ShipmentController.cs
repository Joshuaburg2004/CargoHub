using Microsoft.AspNetCore.Mvc;

[Route("api/shipment")]
public class ShipmentController : Controller
{
    private readonly IShipmentService _shipmentService;

    public ShipmentController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetShipments()
    {
        var shipments = await _shipmentService.GetShipments();
        return Ok(shipments);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetShipmentById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        var shipment = await _shipmentService.GetShipment(id);
        return Ok(shipment);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
    {
        if (shipment == null) return BadRequest();
        await _shipmentService.AddShipment(shipment);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateShipment([FromBody] Shipment shipment)
    {
        if (shipment == null) return BadRequest();
        await _shipmentService.UpdateShipment(shipment);
        return Ok();
    }

    [HttpPut("updateitems")]
    public async Task<IActionResult> UpdateItemsInShipment(Guid id, Guid shipmentid, [FromBody] List<Item> items)
    {
        if (items == null || id == Guid.Empty || shipmentid == Guid.Empty || items == null) return BadRequest();
        await _shipmentService.Update_items_in_Shipment(id, shipmentid, items);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteShipment(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        await _shipmentService.DeleteShipment(id);
        return Ok();
    }
}