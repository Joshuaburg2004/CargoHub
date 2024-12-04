using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;
using CargoHubAlt.Interfaces.InterfacesV1;

namespace CargoHub.Controllers
{
    [ApiController]
    [Route("api/v1/transfers")]
    public class TransferController : Controller
    {
        private readonly ILogger<TransferController> _logger;
        private readonly ITransferServiceV1 _transferservice;

        public TransferController(ITransferServiceV1 transferservice, ILogger<TransferController> logger)
        {
            _transferservice = transferservice;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTransfers()
        {
            List<Transfer>? transfers = await _transferservice.GetTransfers();
            if (transfers == null || transfers.Count == 0)
            {
                _logger.LogInformation("No transfers found");
                return NotFound();
            }
            _logger.LogInformation($"Found {transfers.Count} transfers");
            return Ok(transfers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranfersById([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            Transfer? transfer = await _transferservice.GetTransferById(id);
            if (transfer == null)
            {
                _logger.LogInformation($"Transfer with id:{id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Transfer with id:{id} found");
            return Ok(transfer);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsInTransfer([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            List<TransferItem>? items = await _transferservice.GetItemsInTransfer(id);
            if (items == null || items.Count == 0)
            {
                _logger.LogInformation($"No items found in transfer with id:{id}");
                return NotFound();
            }
            if (items.Count == 1) _logger.LogInformation($"Found {items.Count} item in transfer with id:{id}");
            else _logger.LogInformation($"Found {items.Count} items in transfer with id:{id}");
            return Ok(items);
        }

        [HttpPost()]
        public async Task<IActionResult> AddTransfer([FromBody] Transfer transfer)
        {
            if (transfer == null || transfer.Items == null)
            {
                _logger.LogInformation("Invalid transfer");
                return BadRequest();
            }
            await _transferservice.AddTransfer(transfer);
            _logger.LogInformation($"Created new transfer with id:{transfer.Id}");
            return Created("Created transfer", transfer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTransfer([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            Transfer? transfer = await _transferservice.RemoveTransfer(id);
            if (transfer == null)
            {
                _logger.LogInformation($"Transfer with id:{id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Removed transfer with id:{id}");
            return Ok(transfer);
        }

        [HttpPut("{id}/commit")]
        public async Task<IActionResult> CommitTransfer([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest();
            }
            bool result = await _transferservice.CommitTransferById(id);
            if (!result)
            {
                _logger.LogInformation($"Transfer with id:{id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Committed transfer with id:{id}");
            return Ok($"Processed batch transfer with id:{id}");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransfer([FromRoute] int id, [FromBody] Transfer transfer)
        {
            if (id <= 0 || transfer == null)
            {
                _logger.LogInformation("Invalid id or transfer");
                return BadRequest();
            }
            Transfer? oldTransfer = await _transferservice.UpdateTransfer(id, transfer);
            if (oldTransfer == null)
            {
                _logger.LogInformation($"Transfer with id:{id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Updated transfer with id:{id}");
            return Ok(oldTransfer);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _transferservice.LoadFromJson(path);
            _logger.LogInformation($"Loaded transfers from {path}");
            return Ok();
        }
    }
}