using CargoHubAlt.Services.ServicesV2;
using CargoHubAlt.Interfaces.InterfacesV2;
using Microsoft.AspNetCore.Mvc;

namespace CargoHubAlt.Controllers.ControllersV2{
    [Route("api/v2/analytics")]
    public class AnalyticsController : Controller{
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService service){
            _analyticsService = service;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAnalytics([FromQuery] string? fromdate, [FromQuery] string? todate){
            return Ok(await _analyticsService.GetAnalytics(fromdate == null ? null : DateOnly.Parse(fromdate), todate == null ? null : DateOnly.Parse(todate)));
        }
    }
}