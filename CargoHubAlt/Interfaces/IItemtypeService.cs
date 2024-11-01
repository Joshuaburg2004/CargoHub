public interface IItemTypeService
{
    public Task<Item_type?> GetItemTypeById(int Id);
    public Task<IEnumerable<Item_type>> GetAllItemType();
    public Task<int?> AddItemType(Item_type itemline);
    public Task<Item_type?> UpdateItemType(int Id, Item_type itemline);
    public Task<Item_type?> DeleteItemType(int Id);
}