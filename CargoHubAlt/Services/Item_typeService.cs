using Microsoft.EntityFrameworkCore;

public class ItemTypeService : IItemTypeService
{
    private readonly CargoHubContext _cargoHubContext;


    public ItemTypeService(CargoHubContext context)
    {
        _cargoHubContext = context;
    }

    public async Task<Item_type?> GetItemLineById(Guid Id)
    {
        return await _cargoHubContext.Item_Types.FirstOrDefaultAsync(item_type => item_type.Id == Id);
    }

    public async Task<IEnumerable<Item_type>> GetAllItemType()
    {
        return await _cargoHubContext.Item_Types.ToListAsync();
    }
    public async Task<Guid?> AddItemType(Item_type itemtype)
    {
        Item_type? found = await _cargoHubContext.Item_Types.FirstOrDefaultAsync(x => x.Id == itemtype.Id);
        if (found is null) { return null; }

        await _cargoHubContext.Item_Types.AddAsync(itemtype);
        await _cargoHubContext.SaveChangesAsync();
        return itemtype.Id;
    }

    public async Task<Item_type?> UpdateItemType(Guid Id, Item_type itemtype)
    {
        Item_type? found = await GetItemLineById(Id);
        if (found is null) return null;

        found.Name = itemtype.Name;
        found.Description = itemtype.Description;
        found.UpdatedAt = itemtype.UpdatedAt;

        _cargoHubContext.Item_Types.Update(found);
        await _cargoHubContext.SaveChangesAsync();
        return found;
    }


    public async Task<Item_type?> DeleteItemType(Guid Id)
    {
        Item_type? found = await GetItemLineById(Id);
        if (found is null) return null;

        _cargoHubContext.Item_Types.Remove(found);
        await _cargoHubContext.SaveChangesAsync();
        return found;
    }
}