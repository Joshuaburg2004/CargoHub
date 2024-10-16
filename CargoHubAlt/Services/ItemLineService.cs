using Microsoft.EntityFrameworkCore;

public class ItemLineService: IItemLineService
{
    private readonly CargoHubContext _cargoHubContext;

    public ItemLineService(CargoHubContext context){
        _cargoHubContext = context;
    }

    public async Task<Item_line?> FindItemLine(Guid Id)
    {
        return await this._cargoHubContext.Item_Lines.FirstOrDefaultAsync(item_Line => item_Line.Id == Id);
    }

    public async Task<IEnumerable<Item_line?>> FindManyItemLine(IEnumerable<Guid> Ids)
    {
        List<Item_line?> toReturn = new List<Item_line?>();
        
        foreach (Guid id in Ids)
        {
            toReturn.Append(await this._cargoHubContext.Item_Lines.FirstOrDefaultAsync(item_Line => item_Line.Id == id));
        }
        return toReturn;

    }
    public async Task<IEnumerable<Item_line>> GetAllItemLine()
    {
        return await this._cargoHubContext.Item_Lines.ToListAsync();
    }
    public async Task<Guid?> AddItemLine(Item_line toAdd)
    {
        if (await this._cargoHubContext.Item_Lines.FirstOrDefaultAsync(x => x.Id ==toAdd.Id) is null)
        {
            Guid toReturn = new Guid();
            toAdd.Id = toReturn;
            await this._cargoHubContext.Item_Lines.AddAsync(toAdd);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return toReturn;
            else return null;
        }
        return null;
        

    }
    public async Task<Item_line?> UpdateItemLine(Guid Id, Item_line toUpdate)
    {
        Item_line? found = await this.FindItemLine(Id);
        if (found is null) return null;
        
        this._cargoHubContext.Item_Lines.Update(toUpdate);
        await this._cargoHubContext.SaveChangesAsync();
        return found;
    }


    public async Task<Item_line?> DeleteItemLine(Guid Id)
    {
        Item_line? found = await this.FindItemLine(Id);
        if (found is null) return null;

        this._cargoHubContext.Item_Lines.Remove(found);
        if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
        else return null;
    }
}