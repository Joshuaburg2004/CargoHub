using Microsoft.EntityFrameworkCore;

public class ItemGroupService: IItemGroupService
{
    private readonly CargoHubContext _cargoHubContext;


    public ItemGroupService(CargoHubContext context){
        _cargoHubContext = context;
    }

    public async Task<Item_group?> FindItemGroup(Guid Id)
    {
        return await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(item_group => item_group.Id == Id);
    }
    public async Task<IEnumerable<Item_group?>> FindManyItemGroup(IEnumerable<Guid> Ids)
    {
        List<Item_group?> toReturn = new List<Item_group?>();
        
        foreach (Guid id in Ids)
        {
            toReturn.Append(await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(item_group => item_group.Id == id));
        }
        return toReturn;

    }
    public async Task<IEnumerable<Item_group>> GetAllItemGroup()
    {
        return await this._cargoHubContext.Item_Groups.ToListAsync();
    }
    public async Task<Guid?> AddItemGroup(Item_group toAdd)
    {
        Item_group? found = await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(x => x.Id ==toAdd.Id);

        if (found is null)
        {
            return null;
        }
        Guid toReturn = new Guid();
        toAdd.Id = toReturn;
        
        await this._cargoHubContext.Item_Groups.AddAsync(toAdd);
        if (await this._cargoHubContext.SaveChangesAsync() >= 1) return toReturn;
        else return null;
    }

    public async Task<Item_group?> UpdateItemGroup(Guid Id, Item_group toUpdate)
    {
        Item_group? found = await this.FindItemGroup(Id);
        if (found is null) return null;

        found.Name = toUpdate.Name;
        found.Description = toUpdate.Description;
        found.UpdatedAt = toUpdate.UpdatedAt;
        
        this._cargoHubContext.Item_Groups.Update(found);
        await this._cargoHubContext.SaveChangesAsync();
        return found;
    }


    public async Task<Item_group?> DeleteItemGroup(Guid Id)
    {
        Item_group? found = await this.FindItemGroup(Id);
        if (found is null) return null;

        this._cargoHubContext.Item_Groups.Remove(found);
        if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
        else return null;
    }
}