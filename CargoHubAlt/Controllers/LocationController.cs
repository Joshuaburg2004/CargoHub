using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHub.Controllers
{
    [ApiController]
    [Route("api/v1/locations")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationservice;

        public LocationController(ILocationService locationService)
        {
            _locationservice = locationService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllLocations()
        {
            return Ok(await _locationservice.GetAllLocations());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneLocation([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            Location? location = await _locationservice.GetOneLocation(id);
            if (location is null) return NotFound();
            return Ok(location);
        }

        [HttpPost()]
        public async Task<IActionResult> PostLocation([FromBody] Location toAdd)
        {
            if (toAdd is null) return BadRequest();
            await _locationservice.AddLocation(toAdd);
            return Created("Created location", toAdd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation([FromRoute] int id, [FromBody] Location toUpdate)
        {
            if (id <= 0 || toUpdate is null) return BadRequest();
            await _locationservice.UpdateLocation(id, toUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            await _locationservice.DeleteLocation(id);
            return Ok();
        }
    }
}