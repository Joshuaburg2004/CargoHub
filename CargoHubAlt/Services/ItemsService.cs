using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class ItemsService : IItemsService
    {
        readonly CargoHubContext _context;
        public ItemsService(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await this._context.Items.ToListAsync();
        }

        public async Task<Item?> GetItem(string id)
        {
            return await this._context.Items.FirstOrDefaultAsync(_ => _.uid == id);
        }
        public async Task<IEnumerable<Inventory>> GetInventoryByItem(string id)
        {
            return await this._context.Inventories.Where(_ => _.item_id == id).ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetInventoryTotalsByItem(string id)
        {
            Dictionary<string, int> toReturn = new Dictionary<string, int>();
            toReturn.Add("total_expected", 0);
            toReturn.Add("total_ordered", 0);
            toReturn.Add("total_allocated", 0);
            toReturn.Add("total_available", 0);
            foreach (Inventory inv in await this._context.Inventories.Where(_ => _.item_id == id).ToListAsync())
            {
                toReturn["total_expected"] += inv.total_expected;
                toReturn["total_ordered"] += inv.total_ordered;
                toReturn["total_allocated"] += inv.total_allocated;
                toReturn["total_available"] += inv.total_available;
            }
            return toReturn;
        }
        public async Task<string?> AddItem(Item toAdd)
        {
            await this._context.Items.AddAsync(toAdd);
            if (await this._context.SaveChangesAsync() >= 1) return toAdd.uid;
            else return null;
        }

        public async Task<Item?> UpdateItem(string toUpdate, Item UpdateTo)
        {
            Item? found = await this._context.Items.FindAsync(toUpdate);
            if (found is null) return null;
            found.uid = UpdateTo.uid;
            found.code = UpdateTo.code;
            found.description = UpdateTo.description;
            found.short_description = UpdateTo.short_description;
            found.upc_code = UpdateTo.upc_code;
            found.model_number = UpdateTo.model_number;
            found.commodity_code = UpdateTo.commodity_code;
            found.item_line = UpdateTo.item_line;
            found.item_group = UpdateTo.item_group;
            found.item_type = UpdateTo.item_type;
            found.unit_purchase_quantity = UpdateTo.unit_purchase_quantity;
            found.unit_order_quantity = UpdateTo.unit_order_quantity;
            found.pack_order_quantity = UpdateTo.pack_order_quantity;
            found.supplier_id = UpdateTo.supplier_id;
            found.supplier_code = UpdateTo.supplier_code;
            found.supplier_part_number = UpdateTo.supplier_part_number;
            found.updated_at = UpdateTo.updated_at;


            if (await this._context.SaveChangesAsync() >= 1) return found;
            else return null;
        }

        public async Task<Item?> RemoveItem(string toRemove)
        {
            Item? found = await this._context.Items.FindAsync(toRemove);
            if (found is null) return null;
            this._context.Items.Remove(found);
            if (await this._context.SaveChangesAsync() >= 1) return found;
            else return null;
        }

        public Task<bool> Load()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}