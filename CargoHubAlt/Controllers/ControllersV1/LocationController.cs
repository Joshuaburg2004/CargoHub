using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV1;
using CargoHubAlt.Models;

namespace CargoHub.Controllers.ControllersV1
{
    [ApiController]
    [Route("api/v1/locations")]
    public class LocationControllerV1 : Controller
    {
        private readonly ILocationServiceV1 _locationservice;

        public LocationControllerV1(ILocationServiceV1 locationService)
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
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await _locationservice.LoadFromJson(path);
            return Ok();
        }
    }
}