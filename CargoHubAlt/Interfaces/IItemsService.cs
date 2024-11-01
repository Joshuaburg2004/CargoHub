public interface IItemsService
{
    // TODO:    add types return types Task<bool, int> oid
    //          add parameters

    Task<IEnumerable<Item>> GetAllItems();
    Task<IEnumerable<Item?>> GetItemsBatch(string[] ids);
    Task<Item?> GetItem(Guid id);
    Task<IEnumerable<Item>> GetItemsForItemGroup(Guid id);

    Task<Guid?> AddItem(Item item);
    Task<Item?> UpdateItem(Guid toUpdate, Item UpdateTo);
    Task<Item?> RemoveItem(Guid toRemove);
    Task<bool> Load();
    Task<bool> Save();
}