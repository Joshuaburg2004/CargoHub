using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IInventoryService
    {
        Task<int?> CreateInventory(Inventory inventory);
        Task<Inventory?> GetOneInventory(int id);
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory?> UpdateInventory(int id, Inventory inventory);
        Task<Inventory?> DeleteInventory(int id);
    }
}
