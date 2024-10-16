public interface IItemLineService
{
    public Task<Item_line?> FindItemLine(Guid Id);
    public Task<IEnumerable<Item_line?>> FindManyItemLine(IEnumerable<Guid> Ids);
    public Task<IEnumerable<Item_line>> GetAllItemLine();
    public Task<Guid?> AddItemLine(Item_line toAdd);
    public Task<Item_line?> UpdateItemLine(Guid Id, Item_line toUpdate);
    public Task<Item_line?> DeleteItemLine(Guid Id);
}