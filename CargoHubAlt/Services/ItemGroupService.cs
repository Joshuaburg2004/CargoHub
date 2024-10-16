using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

public class ItemGroupService: IItemGroupService
{
    private readonly CargoHubContext _cargoHubContext;


    public ItemGroupService(CargoHubContext context){
        _cargoHubContext = context;
    }

    public async Task<Item_group?> FindItemGroup(int Id)
    {
        if (Id < 0) return null;
        return await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(item_group => item_group.Id == Id);    }
    public async Task<IEnumerable<Item_group?>> FindManyItemGroup(IEnumerable<int> Ids)
    {
        List<Item_group?> toReturn = new List<Item_group?>();
        
        foreach (int id in Ids)
        {
            if (id < 0) toReturn.Append(null);
            else toReturn.Append(await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(item_group => item_group.Id == id));
        }
        return toReturn;

    }
    public async Task<IEnumerable<Item_group>> GetAllItemGroup()
    {
        return await this._cargoHubContext.Item_Groups.ToListAsync();
    }
    public async Task<bool> AddItemGroup(Item_group toAdd)
    {
        await this._cargoHubContext.Item_Groups.AddAsync(toAdd);
        if (await this._cargoHubContext.SaveChangesAsync() >= 1) return true;
        else return false;
    }
    public async Task<Item_group?> UpdateItemGroup(int Id, Item_group toUpdate)
    {
        Item_group? found = await this.FindItemGroup(Id);
        if (found is null) return null;
        
        this._cargoHubContext.Item_Groups.Update(toUpdate);
        await this._cargoHubContext.SaveChangesAsync();
        return found;
    }


    public async Task<Item_group?> DeleteItemGroup(int Id)
    {
        Item_group? found = await this.FindItemGroup(Id);
        if (found is null) return null;

        this._cargoHubContext.Item_Groups.Remove(found);
        await this._cargoHubContext.SaveChangesAsync();
        return found;
    }
}