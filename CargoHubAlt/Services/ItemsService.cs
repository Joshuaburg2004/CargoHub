using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        return await this._context.Items.FirstOrDefaultAsync(_ => _.Uid == id);
    }
    public async Task<Inventory?> GetInventoryByItem(string id)
    {
        return await this._context.Inventories.FirstOrDefaultAsync(_ => _.Item_id == id);
    }

    public async Task<Dictionary<string, int>> GetInventoryTotalsByItem(string id){
        Dictionary<string, int> toReturn = new Dictionary<string, int>();
        Inventory? found = await this._context.Inventories.FirstOrDefaultAsync(_ => _.Item_id == id);
        if (found is null) {
            toReturn.Add("total_expected", 0);
            toReturn.Add("total_ordered", 0);
            toReturn.Add("total_allocated", 0);
            toReturn.Add("total_available", 0);
            return toReturn;
        }
        toReturn.Add("total_expected", found.Total_expected);
        toReturn.Add("total_ordered", found.Total_ordered);
        toReturn.Add("total_allocated", found.Total_allocated);
        toReturn.Add("total_available", found.Total_available);
        return toReturn;
    }
    public async Task<string?> AddItem(Item toAdd)
    {
        await this._context.Items.AddAsync(toAdd);
        if (await this._context.SaveChangesAsync() >= 1) return toAdd.Uid;
        else return null;
    }

    public async Task<Item?> UpdateItem(string toUpdate, Item UpdateTo)
    {
        Item? found = await this._context.Items.FindAsync(toUpdate);
        if (found is null) return null;
        found.Uid = UpdateTo.Uid;
        found.Code = UpdateTo.Code;
        found.Description = UpdateTo.Description;
        found.ShortDescription = UpdateTo.ShortDescription;
        found.UpcCode = UpdateTo.UpcCode;
        found.ModelNumber = UpdateTo.ModelNumber;
        found.CommodityCode = UpdateTo.CommodityCode;
        found.ItemLine = UpdateTo.ItemLine;
        found.ItemGroup = UpdateTo.ItemGroup;
        found.ItemType = UpdateTo.ItemType;
        found.UnitPurchaseQuantity = UpdateTo.UnitPurchaseQuantity;
        found.UnitOrderQuantity = UpdateTo.UnitOrderQuantity;
        found.PackOrderQuantity = UpdateTo.PackOrderQuantity;
        found.SupplierId = UpdateTo.SupplierId;
        found.SupplierCode = UpdateTo.SupplierCode;
        found.SupplierPartNumber = UpdateTo.SupplierPartNumber;
        found.UpdatedAt = UpdateTo.UpdatedAt;


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