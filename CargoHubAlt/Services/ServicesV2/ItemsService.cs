using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;

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

        public async Task<List<Item>> GetItems(int? pageIndex)
        {
            if(pageIndex == null)
            {
                return await GetItems();
            }
            int page = (int)pageIndex;
            var Items = await _context.Items
                .OrderBy(c => c.Uid)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
            return Items;
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
            if (UpdateTo.Uid != found.Uid)
            {
                found.Uid = UpdateTo.Uid;
                ChangedFields += $"Uid, {found.Uid}, ";
            }
            if (UpdateTo.Code != found.Code)
            {
                found.Code = UpdateTo.Code;
                ChangedFields += $"Code, {found.Code}, ";
            }
            if (UpdateTo.Description != found.Description)
            {
                found.Description = UpdateTo.Description;
                ChangedFields += $"Description, {found.Description}, ";
            }
            if (UpdateTo.ShortDescription != found.ShortDescription)
            {
                found.ShortDescription = UpdateTo.ShortDescription;
                ChangedFields += $"ShortDescription, {found.ShortDescription}, ";
            }
            if (UpdateTo.UpcCode != found.UpcCode)
            {
                found.UpcCode = UpdateTo.UpcCode;
                ChangedFields += $"UpcCode, {found.UpcCode}, ";
            }
            if (UpdateTo.ModelNumber != found.ModelNumber)
            {
                found.ModelNumber = UpdateTo.ModelNumber;
                ChangedFields += $"ModelNumber, {found.ModelNumber}, ";
            }
            if (UpdateTo.CommodityCode != found.CommodityCode)
            {
                found.CommodityCode = UpdateTo.CommodityCode;
                ChangedFields += $"CommodityCode, {found.CommodityCode}, ";
            }
            if (UpdateTo.ItemLine != found.ItemLine)
            {
                found.ItemLine = UpdateTo.ItemLine;
                ChangedFields += $"ItemLine, {found.ItemLine}, ";
            }
            if (UpdateTo.ItemGroup != found.ItemGroup)
            {
                found.ItemGroup = UpdateTo.ItemGroup;
                ChangedFields += $"ItemGroup, {found.ItemGroup}, ";
            }
            if (UpdateTo.ItemType != found.ItemType)
            {
                found.ItemType = UpdateTo.ItemType;
                ChangedFields += $"ItemType, {found.ItemType}, ";
            }
            if (UpdateTo.UnitPurchaseQuantity != found.UnitPurchaseQuantity)
            {
                found.UnitPurchaseQuantity = UpdateTo.UnitPurchaseQuantity;
                ChangedFields += $"UnitPurchaseQuantity, {found.UnitPurchaseQuantity}, ";
            }
            if (UpdateTo.UnitOrderQuantity != found.UnitOrderQuantity)
            {
                found.UnitOrderQuantity = UpdateTo.UnitOrderQuantity;
                ChangedFields += $"UnitOrderQuantity, {found.UnitOrderQuantity}, ";
            }
            if (UpdateTo.PackOrderQuantity != found.PackOrderQuantity)
            {
                found.PackOrderQuantity = UpdateTo.PackOrderQuantity;
                ChangedFields += $"PackOrderQuantity, {found.PackOrderQuantity}, ";
            }
            if (UpdateTo.SupplierId != found.SupplierId)
            {
                found.SupplierId = UpdateTo.SupplierId;
                ChangedFields += $"SupplierId, {found.SupplierId}, ";
            }
            if (UpdateTo.SupplierCode != found.SupplierCode)
            {
                found.SupplierCode = UpdateTo.SupplierCode;
                ChangedFields += $"SupplierCode, {found.SupplierCode}, ";
            }
            if (UpdateTo.SupplierPartNumber != found.SupplierPartNumber)
            {
                found.SupplierPartNumber = UpdateTo.SupplierPartNumber;
                ChangedFields += $"SupplierPartNumber, {found.SupplierPartNumber}, ";
            }
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
                List<JsonItem>? items = JsonSerializer.Deserialize<List<JsonItem>>(json);
                if (items == null)
                {
                    return;
                }
                var transaction = _context.Database.BeginTransaction();
                foreach (JsonItem jsonItem in items)
                {
                    Item item = jsonItem.ToItem();
                    await SaveToDatabase(item);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
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
            return item.Uid;
        }
    }
}