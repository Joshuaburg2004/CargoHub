using Microsoft.AspNetCore.Mvc;

[Route("api/v1/transfers")]
public class TransferController : Controller
{
    private readonly ITransfer _transferservice;

    public TransferController(ITransfer transferservice)
    {
        _transferservice = transferservice;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetTransfers() => Ok(await _transferservice.GetTransfers());

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetTranfersById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        return Ok(await _transferservice.GetTransferById(id));
    }

    [HttpGet("getitems")]
    public async Task<IActionResult> GetItemsInTransfer(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        return Ok(await _transferservice.GetItemsInTransfer(id));
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTransfer([FromBody] Transfer transfer)
    {
        if (transfer == null) return BadRequest();
        return Ok(await _transferservice.AddTransfer(transfer));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> RemoveTransfer([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        return Ok(await _transferservice.RemoveTransfer(id));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateTransfer(Guid id, Transfer transfer)
    {
        if (id == Guid.Empty && transfer == null) return BadRequest(); // && moet of zijn maar staat niet op dit toetsenbord zal dit later aanpassen
        return Ok(await _transferservice.UpdateTransfer(id, transfer));
    }
}