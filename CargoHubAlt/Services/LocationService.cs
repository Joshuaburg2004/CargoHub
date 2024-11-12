using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class LocationService : ILocationService
    {
        readonly CargoHubContext _context;
        public LocationService(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllLocations() => await _context.Locations.ToListAsync();

        public async Task<Location?> GetOneLocation(int id) => await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int?> AddLocation(Location Location)
        {
            await _context.Locations.AddAsync(Location);
            if (await _context.SaveChangesAsync() >= 0) return Location.Id;
            else return null;
        }

        public async Task<Location?> UpdateLocation(int id, Location Location)
        {
            Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
            if (location is null) { return null; }

            location.Id = Location.Id;
            location.Warehouse_Id = Location.Warehouse_Id;
            location.Code = Location.Code;
            location.Name = Location.Name;
            location.Updated_At = Location.Updated_At;

            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location?> DeleteLocation(int id)
        {
            Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
            if (location is null) { return null; }

            _context.Remove(location);
            await _context.SaveChangesAsync();
            return location;
        }
    }
}