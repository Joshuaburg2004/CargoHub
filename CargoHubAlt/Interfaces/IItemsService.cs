public interface IItemsService
{
    // TODO:    add types return types Task<bool, int> oid
    //          add parameters
    Task<Item[]> GetItems(string[] ids);
    Task GetItem(string id);
    Task GetItemsForItemLine();
    Task GetItemsForItemGroup();
    Task GetItemsForItemType();
    Task GetItemsForSupplier();
    Task AddItem();
    Task UpdateItem();
    Task RemoveItem();
    Task Load();
    Task Save();
}