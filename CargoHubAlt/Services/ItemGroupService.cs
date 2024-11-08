using Microsoft.EntityFrameworkCore;

public class ItemGroupService: IItemGroupService
{
    private readonly CargoHubContext _cargoHubContext;


    public ItemGroupService(CargoHubContext context){
        _cargoHubContext = context;
    }

    public async Task<Item_group?> FindItemGroup(int Id)
    {
        return await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(item_group => item_group.Id == Id);
    }
    public async Task<IEnumerable<Item_group?>> FindManyItemGroup(IEnumerable<int> Ids)
    {
        List<Item_group?> toReturn = new List<Item_group?>();
        
        foreach (int id in Ids)
        {
            toReturn.Append(await this._cargoHubContext.Item_Groups.FirstOrDefaultAsync(item_group => item_group.Id == id));
        }
        return toReturn;

    }
    public async Task<IEnumerable<Item_group>> GetAllItemGroup()
    {
        return await this._cargoHubContext.Item_Groups.ToListAsync();
    }

    public async Task<IEnumerable<Item>?> GetItemsfromItemGroupById(int id)
    {
        if (id < 0) return null;
        return await this._cargoHubContext.Items.Where(_ => _.item_group == id).ToListAsync();
    }


    public async Task<int?> AddItemGroup(Item_group linetype)
    {
        await _cargoHubContext.Item_Groups.AddAsync(linetype);
        await _cargoHubContext.SaveChangesAsync();
        return linetype.Id;
    }

    public async Task<Item_group?> UpdateItemGroup(int Id, Item_group toUpdate)
    {
        Item_group? found = await this.FindItemGroup(Id);
        if (found is null) return null;

        found.Name = toUpdate.Name;
        found.Description = toUpdate.Description;
        found.Updated_At = toUpdate.Updated_At;
        
        this._cargoHubContext.Item_Groups.Update(found);
        await this._cargoHubContext.SaveChangesAsync();
        return found;
    }


    public async Task<Item_group?> DeleteItemGroup(int Id)
    {
        Item_group? found = await this.FindItemGroup(Id);
        if (found is null) return null;

        this._cargoHubContext.Item_Groups.Remove(found);
        if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
        else return null;
    }
}