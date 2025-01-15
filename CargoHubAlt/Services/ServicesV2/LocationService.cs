using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;
using CargoHubAlt.Migrations;

namespace CargoHubAlt.Services.ServicesV2
{
    public class LocationServiceV2 : ILocationServiceV2
    {
        readonly CargoHubContext _context;
        public LocationServiceV2(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllLocations() => await _context.Locations.ToListAsync();
        public async Task<List<Location>> GetAllLocations(int? pageIndex)
        {
            if (pageIndex == null) return await GetAllLocations();
            int page = (int)pageIndex;
            return await _context.Locations
                .OrderBy(c => c.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
        }

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

        public async Task DisperseAllInventoriesOverLocations(){
            List<Inventory> inventories = await _context.Inventories.ToListAsync();
            foreach(Inventory inventory in inventories) {
                List<Location> locations = inventory.Locations.Select(x => 
                    _context.Locations.Where(l => l.Id == x).First()
                ).ToList();
                int amount = inventory.TotalAvailable / locations.Count;
                foreach (Location location in locations) {
                    if(location.localInventories == null){
                        location.localInventories = new List<LocalInventory>();
                    }
                    location.localInventories.Add(new LocalInventory { InventoryId = inventory.Id, Amount = amount });
                }
                _context.Locations.UpdateRange(locations);
            }
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