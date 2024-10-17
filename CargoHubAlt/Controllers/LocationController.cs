using Microsoft.AspNetCore.Mvc;

[Route("api/v1/locations")]
public class LocationController : Controller
{
    private readonly ILocationService _locationservice;

    public LocationController(ILocationService locationService)
    {
        this._locationservice = locationService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllLocations()
    {
        return Ok(await this._locationservice.GetAllLocations());
    }

    [HttpGet()]
    public async Task<IActionResult> GetOneLocation([FromQuery] Guid id)
    {
        Location? found = await this._locationservice.GetOneLocation(id);
        if (found is null) return NotFound($"id not found {id}");
        return Ok(found);
    }

    [HttpGet("batch")]
    public async Task<IActionResult> GetManyLocations(IEnumerable<Guid> ids)
    {
        IEnumerable<Location?> found = await this._locationservice.GetBatchLocation(ids);
        return Ok(found);
    }

    [HttpPost()]
    public async Task<IActionResult> PostLocation(Location toAdd)
    {
        Guid? success = await this._locationservice.AddLocation(toAdd);
        if (success is null) return BadRequest("something went wrong");
        else return Ok(success);
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateLocation(Guid id, Location toUpdate)
    {
        Location? success = await this._locationservice.UpdateLocation(id, toUpdate);
        if (success is null) return BadRequest();
        else return Ok(success);
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        Location? success = await this._locationservice.DeleteLocation(id);
        if (success is null) return BadRequest();
        else return Ok(success);
    }
}