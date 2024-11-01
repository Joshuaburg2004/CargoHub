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

    public async Task<IEnumerable<Item>> GetAllItems()
    {
        return await this._context.Items.ToListAsync();
    }

    public async Task<IEnumerable<Item?>> GetItems(string[] ids)
    {
        List<Item?> toReturn = new(ids.Count());
        foreach (string one in ids)
        {
            if (Guid.TryParse(one, out Guid item))
            {
                toReturn.Add(await this._context.Items.FirstOrDefaultAsync(_ => _.Id == item));
            }
            else toReturn.Add(null);
        }
        return toReturn;
    }

    public async Task<Item?> GetItem(Guid id)
    {
        return await this._context.Items.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<IEnumerable<Item>> GetItemsForItemLine(Guid id)
    {
        return await this._context.Items.Where(_ => _.ItemLine == id).ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsForItemGroup(Guid id)
    {
        return await this._context.Items.Where(_ => _.ItemGroup == id).ToListAsync();
    }


    public async Task<Guid?> AddItem(Item toAdd)
    {
        Guid toReturn = Guid.NewGuid();
        toAdd.Id = toReturn;

        await this._context.Items.AddAsync(toAdd);
        if (await this._context.SaveChangesAsync() >= 1) return toReturn;
        else return null;
    }

    public async Task<IEnumerable<Item?>> GetItemsBatch(string[] ids)
    {
        List<Item?> toReturn = new(ids.Count());
        foreach (string one in ids)
        {
            if (Guid.TryParse(one, out Guid item))
            {
                toReturn.Add(await this._context.Items.FirstOrDefaultAsync(_ => _.Id == item));
            }
            else toReturn.Add(null);
        }
        return toReturn;
}

    public async Task<Item?> UpdateItem(Guid toUpdate, Item UpdateTo)
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

    public async Task<Item?> RemoveItem(Guid toRemove)
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