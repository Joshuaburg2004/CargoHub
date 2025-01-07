using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV2
{
    public class ItemsServiceV2 : IItemsServiceV2
    {
        readonly CargoHubContext _context;
        public ItemsServiceV2(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItems()
        {
            return await this._context.Items.ToListAsync();
        }

        public async Task<Item?> GetItem(string id)
        {
            return await this._context.Items.FirstOrDefaultAsync(_ => _.Uid == id);
        }
        public async Task<List<Inventory>> GetInventoryByItem(string id)
        {
            return await this._context.Inventories.Where(_ => _.ItemId == id).ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetInventoryTotalsByItem(string id)
        {
            Dictionary<string, int> toReturn = new Dictionary<string, int>();
            toReturn.Add("total_expected", 0);
            toReturn.Add("total_ordered", 0);
            toReturn.Add("total_allocated", 0);
            toReturn.Add("total_available", 0);
            foreach (Inventory inv in await this._context.Inventories.Where(_ => _.ItemId == id).ToListAsync())
            {
                toReturn["total_expected"] += inv.TotalExpected;
                toReturn["total_ordered"] += inv.TotalOrdered;
                toReturn["total_allocated"] += inv.TotalAllocated;
                toReturn["total_available"] += inv.TotalAvailable;
            }
            return toReturn;
        }
        public async Task<string?> AddItem(Item? toAdd)
        {
            if (toAdd is null) return null;
            if (await GetItem(toAdd.Uid) != null) return "Existed";
            await this._context.Items.AddAsync(toAdd);
            if (await this._context.SaveChangesAsync() >= 1) return toAdd.Uid;
            else return null;
        }

        public async Task<string?> UpdateItem(string toUpdate, Item UpdateTo)
        {
            Item? found = await this._context.Items.FindAsync(toUpdate);
            var ChangedFields = "";
            if (found is null) return null;
            if (UpdateTo.Uid != null) { found.Uid = UpdateTo.Uid; ChangedFields += "Uid, "; }
            if (UpdateTo.Code != null) { found.Code = UpdateTo.Code; ChangedFields += "Code, "; }
            if (UpdateTo.Description != null) { found.Description = UpdateTo.Description; ChangedFields += "Description, "; }
            if (UpdateTo.ShortDescription != null) { found.ShortDescription = UpdateTo.ShortDescription; ChangedFields += "ShortDescription, "; }
            if (UpdateTo.UpcCode != null) { found.UpcCode = UpdateTo.UpcCode; ChangedFields += "UpcCode, "; }
            if (UpdateTo.ModelNumber != null) { found.ModelNumber = UpdateTo.ModelNumber; ChangedFields += "ModelNumber, "; }
            if (UpdateTo.CommodityCode != null) { found.CommodityCode = UpdateTo.CommodityCode; ChangedFields += "CommodityCode, "; }
            if (UpdateTo.SupplierCode != null) { found.SupplierCode = UpdateTo.SupplierCode; ChangedFields += "SupplierCode, "; }
            if (UpdateTo.SupplierPartNumber != null) { found.SupplierPartNumber = UpdateTo.SupplierPartNumber; ChangedFields += "SupplierPartNumber, "; }
            found.UpdatedAt = UpdateTo.UpdatedAt;


            if (await this._context.SaveChangesAsync() >= 1) return ChangedFields;
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

        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Item>? items = JsonSerializer.Deserialize<List<Item>>(json);
                if (items == null)
                {
                    return;
                }
                foreach (Item item in items)
                {
                    await SaveToDatabase(item);
                }
            }
        }
        public async Task<string> SaveToDatabase(Item item)
        {
            if (item is null)
            {
                return "Please provide an item to add.";
            }
            if (item.Code == null) { item.Code = "N/A"; }
            if (item.Description == null) { item.Description = "N/A"; }
            if (item.ShortDescription == null) { item.ShortDescription = "N/A"; }
            if (item.UpcCode == null) { item.UpcCode = "N/A"; }
            if (item.ModelNumber == null) { item.ModelNumber = "N/A"; }
            if (item.CommodityCode == null) { item.CommodityCode = "N/A"; }
            if (item.SupplierCode == null) { item.SupplierCode = "N/A"; }
            if (item.SupplierPartNumber == null) { item.SupplierPartNumber = "N/A"; }
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return item.Uid;
        }
    }
}