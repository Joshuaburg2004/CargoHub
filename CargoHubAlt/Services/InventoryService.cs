using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly CargoHubContext _cargoHubContext;
        public InventoryService(CargoHubContext context)
        {
            _cargoHubContext = context;
        }
        public async Task<Inventory?> GetOneInventory(int id)
        {
            return await this._cargoHubContext.Inventories.FirstOrDefaultAsync(inventory => inventory.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await this._cargoHubContext.Inventories.ToListAsync();
        }

        public async Task<int?> CreateInventory(Inventory inventory)
        {
            inventory.Created_at = Inventory.GetTimeStamp();
            inventory.Updated_at = Inventory.GetTimeStamp();
            await this._cargoHubContext.Inventories.AddAsync(inventory);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return inventory.Id;
            else return null;
        }

        public async Task<Inventory?> UpdateInventory(int id, Inventory inventory)
        {
            Inventory? found = await this._cargoHubContext.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (found is null) return null;
            var orig_found = found;
            found.Item_id = inventory.Item_id;
            found.Description = inventory.Description;
            found.Item_reference = inventory.Item_reference;
            found.Locations = inventory.Locations;
            found.Total_on_hand = inventory.Total_on_hand;
            found.Total_expected = inventory.Total_expected;
            found.Total_ordered = inventory.Total_ordered;
            found.Total_allocated = inventory.Total_allocated;
            found.Total_available = inventory.Total_available;
            found.Updated_at = Inventory.GetTimeStamp();
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
    }
}