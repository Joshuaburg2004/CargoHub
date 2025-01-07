using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers.ControllersV2
{
    [ApiController]
    [Route("api/v2/backup")]
    public class BackupController : Controller
    {
        private readonly IBackupService _backupService;
        public BackupController(IBackupService backupService)
        {
            _backupService = backupService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateBackup()
        {
            var succes = await _backupService.CreateBackup();
            return Ok(succes.Item2);
        }

        [HttpPost("{toUpload}")]
        public async Task<IActionResult> uploadBackup([FromRoute] string toUpload)
        {
            var succes = await _backupService.UploadBackupFull(toUpload);
            if (succes.Item1 == false) return BadRequest(succes.Item2);
            return Ok(succes.Item2);
        }
    }
}