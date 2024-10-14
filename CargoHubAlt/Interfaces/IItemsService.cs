public interface IItemsService
{
    // TODO:    add types return types Task<bool, int> oid
    //          add parameters
    Task GetItems(Guid id);
    Task GetItem();
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