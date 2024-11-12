using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;
using System.Text.Json;

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
            return await this._cargoHubContext.Inventories.FirstOrDefaultAsync(inventory => inventory.id == id);
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await this._cargoHubContext.Inventories.ToListAsync();
        }

        public async Task<int?> CreateInventory(Inventory inventory)
        {
            inventory.created_at = Inventory.GetTimeStamp();
            inventory.updated_at = Inventory.GetTimeStamp();
            await this._cargoHubContext.Inventories.AddAsync(inventory);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return inventory.id;
            else return null;
        }

        public async Task<Inventory?> UpdateInventory(int id, Inventory inventory)
        {
            Inventory? found = await this._cargoHubContext.Inventories.FirstOrDefaultAsync(x => x.id == id);
            if (found is null) return null;
            var orig_found = found;
            found.item_id = inventory.item_id;
            found.description = inventory.description;
            found.item_reference = inventory.item_reference;
            found.locations = inventory.locations;
            found.total_on_hand = inventory.total_on_hand;
            found.total_expected = inventory.total_expected;
            found.total_ordered = inventory.total_ordered;
            found.total_allocated = inventory.total_allocated;
            found.total_available = inventory.total_available;
            found.updated_at = Inventory.GetTimeStamp();
            this._cargoHubContext.Inventories.Update(found);
            await this._cargoHubContext.SaveChangesAsync();
            return orig_found;
        }

        public async Task<Inventory?> DeleteInventory(int id)
        {
            Inventory? found = await this._cargoHubContext.Inventories.FirstOrDefaultAsync(x => x.id == id);
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
        public async Task<int> SaveToDatabase(Inventory inventory){
            if(inventory is null){
                return -1;
            }
            if(inventory.item_id == null){inventory.item_id = "N/A";}
            if(inventory.description == null){inventory.description = "N/A";}
            if(inventory.item_reference == null){inventory.item_reference = "N/A";}
            await _cargoHubContext.Inventories.AddAsync(inventory);
            await _cargoHubContext.SaveChangesAsync();
            return inventory.id;   
        }
    }
}