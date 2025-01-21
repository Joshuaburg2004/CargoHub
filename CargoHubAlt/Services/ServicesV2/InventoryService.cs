using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV2
{
    public class InventoryServiceV2 : IInventoryServiceV2
    {
        private readonly CargoHubContext _cargoHubContext;
        public InventoryServiceV2(CargoHubContext context)
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

        public async Task<List<Inventory>> GetAllInventories(int? pageIndex)
        {
            if (pageIndex == null)
            {
                return await GetAllInventories();
            }
            int page = (int)pageIndex;
            return await this._cargoHubContext.Inventories
                .OrderBy(inventory => inventory.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
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
            found.LowStockThreshold = inventory.LowStockThreshold;
            found.IsLowStock = inventory.TotalOnHand <= inventory.LowStockThreshold;
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

        // Returns products with TotalOnHand ≤ customThreshold, or all low-stock products if no threshold is given.
        public async Task<List<Inventory>> GetLowStock(int? customThreshold = null)
        {
            if (!customThreshold.HasValue)
            {
                return await _cargoHubContext.Inventories
                    .Where(inventory => inventory.IsLowStock == true)
                    .ToListAsync();
            }
            else
            {
                return await _cargoHubContext.Inventories
                    .Where(inventory => inventory.TotalAvailable <= customThreshold.Value)
                    .ToListAsync();
            }
        }

        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonInventory>? inventorys = JsonSerializer.Deserialize<List<JsonInventory>>(json);
                if (inventorys == null)
                {
                    return;
                }
                var transaction = _cargoHubContext.Database.BeginTransaction();
                foreach (JsonInventory jsonInventory in inventorys)
                {
                    Inventory inventory = jsonInventory.ToInventory();
                    await SaveToDatabase(inventory);
                }
                await _cargoHubContext.SaveChangesAsync();
                await transaction.CommitAsync();
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
            return inventory.Id;
        }
    }
}