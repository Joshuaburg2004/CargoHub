using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHub.Controllers
{
    [ApiController]
    [Route("api/v1/transfers")]
    public class TransferController : Controller
    {
        private readonly ITransferService _transferservice;

        public TransferController(ITransferService transferservice)
        {
            _transferservice = transferservice;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTransfers()
        {
            List<Transfer>? transfers = await _transferservice.GetTransfers();
            if (transfers == null || transfers.Count == 0) return NotFound();

            return Ok(transfers);
        }

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

        [HttpPut("{id}/commit")]
        public async Task<IActionResult> CommitTransfer([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            bool result = await _transferservice.CommitTransferById(id);
            if (!result) return NotFound();
            return Ok($"Processed batch transfer with id:{id}");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransfer([FromRoute] int id, [FromBody] Transfer transfer)
        {
            if (id <= 0 || transfer == null) return BadRequest();
            Transfer? oldTransfer = await _transferservice.UpdateTransfer(id, transfer);
            if (oldTransfer == null) return NotFound();
            return Ok(oldTransfer);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _transferservice.LoadFromJson(path);
            return Ok();
        }
    }
}