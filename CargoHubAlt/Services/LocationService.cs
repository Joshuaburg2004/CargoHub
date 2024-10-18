using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

public class LocationService : ILocationService
{
    readonly CargoHubContext _context;
    public LocationService(CargoHubContext context)
    {
        this._context = context;
    }

    public async Task<List<Location>> GetAllLocations()
    {
        return await this._context.Locations.ToListAsync();
    }

    public async Task<Location?> GetOneLocation(Guid id)
    {
        return await this._context.Locations.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Location?>> GetBatchLocation(IEnumerable<Guid> Ids)
    {
        List<Location> found = new();
        foreach (Guid id in Ids)
        {
            found.Append(await this._context.Locations.FirstOrDefaultAsync(x => x.Id == id));
        }
        return found;
    }
    public async Task<Guid?> AddLocation(Location Location)
    {
        await this._context.Locations.AddAsync(Location);
        if (await this._context.SaveChangesAsync() >= 0) return Location.Id;
        else return null;
    }
    public async Task<Location?> UpdateLocation(Guid id, Location Location)
    {
        Location? found = await this._context.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (found is null)
        {
            return null;
        }

        found.Code = Location.Code;
        found.Name = Location.Name;
        found.UpdatedAt = Location.UpdatedAt;

        this._context.Locations.Update(found);
        await this._context.SaveChangesAsync();
        return found;
    }
    public async Task<Location?> DeleteLocation(Guid id)
    {
        Location? found = await this._context.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (found is null)
        {
            return null;
        }

        this._context.Remove(found);
        await this._context.SaveChangesAsync();
        return found;
    }

}