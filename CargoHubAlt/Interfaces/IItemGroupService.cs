public interface IItemGroupService
{
    public Task<Item_group?> FindItemGroup(int Id);
    public Task<IEnumerable<Item_group?>> FindManyItemGroup(IEnumerable<int> Ids);
    public Task<IEnumerable<Item_group>> GetAllItemGroup();
    public Task<bool> AddItemGroup(Item_group toAdd);
    public Task<Item_group?> UpdateItemGroup(int Id, Item_group toUpdate);
    public Task<Item_group?> DeleteItemGroup(int Id);
}