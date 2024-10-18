public interface IItemGroupService
{
    public Task<Item_group?> FindItemGroup(Guid Id);
    public Task<IEnumerable<Item_group?>> FindManyItemGroup(IEnumerable<Guid> Ids);
    public Task<IEnumerable<Item_group>> GetAllItemGroup();
    public Task<Guid?> AddItemGroup(Item_group toAdd);
    public Task<Item_group?> UpdateItemGroup(Guid Id, Item_group toUpdate);
    public Task<Item_group?> DeleteItemGroup(Guid Id);
}