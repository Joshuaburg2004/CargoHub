public interface IItemTypeService
{
    public Task<Item_type?> GetItemLineById(Guid Id);
    public Task<IEnumerable<Item_type>> GetAllItemType();
    public Task<Guid?> AddItemType(Item_type itemline);
    public Task<Item_type?> UpdateItemType(Guid Id, Item_type itemline);
    public Task<Item_type?> DeleteItemType(Guid Id);
}