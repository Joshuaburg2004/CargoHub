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
        throw new NotImplementedException();
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetShipmentById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
    {
        throw new NotImplementedException();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateShipment([FromBody] Shipment shipment)
    {
        throw new NotImplementedException();
    }

    [HttpPut("updateitems")]
    public async Task<IActionResult> UpdateItemsInShipment(int id, int shipmentid, [FromBody] List<Item> items)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteShipment(int id)
    {
        throw new NotImplementedException();
    }
}