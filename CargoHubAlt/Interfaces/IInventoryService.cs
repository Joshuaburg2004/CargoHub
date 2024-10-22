public interface IInventoryService
{
    Task<Inventory> CreateInventory(Inventory inventory);
    Task<Inventory> GetInventory(Guid id);
    Task<List<Inventory>> GetInventories();
    Task<Inventory> UpdateInventory(Guid id, Inventory inventory);
    Task<Inventory> DeleteInventory(Guid id);
}