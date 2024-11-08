using Microsoft.AspNetCore.Mvc;

[Route("api/v1/transfers")]
public class TransferController : Controller
{
    private readonly ITransferService _transferservice;

    public TransferController(ITransferService transferservice)
    {
        _transferservice = transferservice;
    }

    [HttpGet()]
    public async Task<IActionResult> GetTransfers() => Ok(await _transferservice.GetTransfers());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTranfersById([FromRoute] int id)
    {
        if (id <= 0) return BadRequest();
        Transfer? transfer = await _transferservice.GetTransferById(id);
        if (transfer == null) return NotFound();
        return Ok(transfer);
    }

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsInTransfer([FromRoute] int id)
    {
        if (id <= 0) return BadRequest();
        List<TransferItem>? items = await _transferservice.GetItemsInTransfer(id);
        if (items == null || items.Count == 0) return NotFound();
        return Ok(items);
    }

    [HttpPost()]
    public async Task<IActionResult> AddTransfer([FromBody] Transfer transfer)
    {
        if (transfer == null || transfer.Items == null) return BadRequest();
        await _transferservice.AddTransfer(transfer);
        return Created("Created transfer", transfer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTransfer([FromRoute] int id)
    {
        if (id <= 0) return BadRequest();
        Transfer? transfer = await _transferservice.RemoveTransfer(id);
        if (transfer == null) return NotFound();
        return Ok(transfer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransfer([FromRoute] int id, [FromBody] Transfer transfer)
    {
        if (id <= 0 || transfer == null) return BadRequest();
        Transfer? oldTransfer = await _transferservice.UpdateTransfer(id, transfer);
        if (oldTransfer == null) return NotFound();
        return Ok(oldTransfer);
    }
}