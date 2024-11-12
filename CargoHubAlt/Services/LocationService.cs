using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;
using System.Text.Json;

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

        public async Task<Location?> GetOneLocation(int id) => await _context.Locations.FirstOrDefaultAsync(x => x.id == id);

        public async Task<int?> AddLocation(Location Location)
        {
            await _context.Locations.AddAsync(Location);
            if (await _context.SaveChangesAsync() >= 0) return Location.id;
            else return null;
        }

        public async Task<Location?> UpdateLocation(int id, Location Location)
        {
            Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.id == id);
            if (location is null) { return null; }

            location.id = Location.id;
            location.warehouse_id = Location.warehouse_id;
            location.code = Location.code;
            location.name = Location.name;
            location.updated_at = Location.updated_at;

            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location?> DeleteLocation(int id)
        {
            Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.id == id);
            if (location is null) { return null; }

            _context.Remove(location);
            await _context.SaveChangesAsync();
            return location;
        }
    public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Location>? locations = JsonSerializer.Deserialize<List<Location>>(json);
                if (locations == null)
                {
                    return;
                }
                foreach (Location location in locations)
                {
                    await SaveToDatabase(location);
                }
            }
        }
        public async Task<int> SaveToDatabase(Location location){
            if(location is null){
                return -1;
            }
            if(location.name == null){location.name = "N/A";}
            if(location.code == null){location.code = "N/A";}
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return location.id;
        }
    }
}