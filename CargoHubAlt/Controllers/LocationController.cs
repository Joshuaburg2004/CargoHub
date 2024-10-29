using Microsoft.AspNetCore.Mvc;

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
        return Ok(await _locationservice.GetOneLocation(id));
    }

    [HttpPost()]
    public async Task<IActionResult> PostLocation([FromBody] Location toAdd)
    {
        if (toAdd is null) return BadRequest();
        await _locationservice.AddLocation(toAdd);
        return Ok();
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