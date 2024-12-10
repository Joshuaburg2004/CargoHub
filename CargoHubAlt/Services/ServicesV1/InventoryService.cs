using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV1;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV1
{
    public class InventoryServiceV1 : IInventoryServiceV1
    {
        private readonly CargoHubContext _cargoHubContext;
        public InventoryServiceV1(CargoHubContext context)
        {
            _cargoHubContext = context;
        }
        public async Task<Inventory?> GetOneInventory(int id)
        {
            return await this._cargoHubContext.Inventories.FirstOrDefaultAsync(inventory => inventory.Id == id);
        }

        public async Task<List<Inventory>> GetAllInventories()
        {
            return await this._cargoHubContext.Inventories.ToListAsync();
        }

        public async Task<int?> CreateInventory(Inventory inventory)
        {
            if (inventory is null) { return null; }
            if (await GetOneInventory(inventory.Id) != null) { return -1; }
            if (inventory.Id <= 0) { return -2; }
            inventory.CreatedAt = Inventory.GetTimeStamp();
            inventory.UpdatedAt = Inventory.GetTimeStamp();
            await this._cargoHubContext.Inventories.AddAsync(inventory);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return inventory.Id;
            else return null;
        }

        public async Task<Inventory?> UpdateInventory(int id, Inventory inventory)
        {
            Inventory? found = await this._cargoHubContext.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (found is null) return null;
            var orig_found = found;
            found.ItemId = inventory.ItemId;
            found.Description = inventory.Description;
            found.ItemReference = inventory.ItemReference;
            found.Locations = inventory.Locations;
            found.TotalOnHand = inventory.TotalOnHand;
            found.TotalExpected = inventory.TotalExpected;
            found.TotalOrdered = inventory.TotalOrdered;
            found.TotalAllocated = inventory.TotalAllocated;
            found.TotalAvailable = inventory.TotalAvailable;
            found.UpdatedAt = Inventory.GetTimeStamp();
            this._cargoHubContext.Inventories.Update(found);
            await this._cargoHubContext.SaveChangesAsync();
            return orig_found;
        }

        public async Task<Inventory?> DeleteInventory(int id)
        {
            Inventory? found = await this._cargoHubContext.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (found is null) return null;
            this._cargoHubContext.Inventories.Remove(found);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
            else return null;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Inventory>? inventorys = JsonSerializer.Deserialize<List<Inventory>>(json);
                if (inventorys == null)
                {
                    return;
                }
                foreach (Inventory inventory in inventorys)
                {
                    await SaveToDatabase(inventory);
                }
            }
        }
        public async Task<int> SaveToDatabase(Inventory inventory)
        {
            if (inventory is null)
            {
                return -1;
            }
            if (inventory.ItemId == null) { inventory.ItemId = "N/A"; }
            if (inventory.Description == null) { inventory.Description = "N/A"; }
            if (inventory.ItemReference == null) { inventory.ItemReference = "N/A"; }
            await _cargoHubContext.Inventories.AddAsync(inventory);
            await _cargoHubContext.SaveChangesAsync();
            return inventory.Id;
        }
    }
}