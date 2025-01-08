using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV1;
using System.Text.Json;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV1
{
    public class LocationServiceV1 : ILocationServiceV1
    {
        readonly CargoHubContext _context;
        public LocationServiceV1(CargoHubContext context)
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
            location.WarehouseId = Location.WarehouseId;
            location.Code = Location.Code;
            location.Name = Location.Name;
            location.UpdatedAt = Location.UpdatedAt;

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
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonLocation>? locations = JsonSerializer.Deserialize<List<JsonLocation>>(json);
                if (locations == null)
                {
                    return;
                }
                var transaction = _context.Database.BeginTransaction();
                foreach (JsonLocation jsonLocation in locations)
                {
                    Location location = jsonLocation.ToLocation();
                    await SaveToDatabase(location);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<int> SaveToDatabase(Location location)
        {
            if (location is null)
            {
                return -1;
            }
            if (location.Name == null) { location.Name = "N/A"; }
            if (location.Code == null) { location.Code = "N/A"; }
            await _context.Locations.AddAsync(location);
            return location.Id;
        }
    }
}