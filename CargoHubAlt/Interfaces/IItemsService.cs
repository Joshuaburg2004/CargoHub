public interface IItemsService
{
    Task<IEnumerable<Item>> GetItems();
    Task<Item?> GetItem(string id);
    Task<Inventory?> GetInventoryByItem(string id);
    Task<Dictionary<string, int>> GetInventoryTotalsByItem(string id);
    Task<string?> AddItem(Item item);
    Task<Item?> UpdateItem(string toUpdate, Item UpdateTo);
    Task<Item?> RemoveItem(string toRemove);
}