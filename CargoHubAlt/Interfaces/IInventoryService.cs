public interface IInventoryService
{
    Task<Inventory?> CreateInventory(Inventory inventory);
    Task<Inventory?> FindInventory(Guid id);
    Task<IEnumerable<Inventory>> FindManyInventories(Guid[] ids);
    Task<IEnumerable<Inventory>> GetAllInventories();
    Task<Inventory?> UpdateInventory(Guid id, Inventory inventory);
    Task<Inventory?> DeleteInventory(Guid id);
}