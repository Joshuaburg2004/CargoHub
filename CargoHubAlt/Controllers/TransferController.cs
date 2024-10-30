using Microsoft.AspNetCore.Mvc;

[Route("api/v1/transfers")]
public class TransferController : Controller
{
    private readonly ITransfer _transferservice;

    public TransferController(ITransfer transferservice)
    {
        _transferservice = transferservice;
    }

    [HttpGet()]
    public async Task<IActionResult> GetTransfers() => Ok(await _transferservice.GetTransfers());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTranfersById([FromRoute] int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _transferservice.GetTransferById(id));
    }

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsInTransfer([FromRoute] int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _transferservice.GetItemsInTransfer(id));
    }

    [HttpPost()]
    public async Task<IActionResult> AddTransfer([FromBody] Transfer transfer)
    {
        if (transfer == null || transfer.Items == null) return BadRequest();
        return Ok(await _transferservice.AddTransfer(transfer));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTransfer([FromRoute] int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _transferservice.RemoveTransfer(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransfer([FromRoute] int id, [FromBody] Transfer transfer)
    {
        if (id <= 0 && transfer == null) return BadRequest(); // && moet of zijn maar staat niet op dit toetsenbord zal dit later aanpassen
        return Ok(await _transferservice.UpdateTransfer(id, transfer));
    }
}