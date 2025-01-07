using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IInventoryServiceV2
    {
        Task<int?> CreateInventory(Inventory inventory);
        Task<Inventory?> GetOneInventory(int id);
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory?> UpdateInventory(int id, Inventory inventory);
        Task<Inventory?> DeleteInventory(int id);
        Task<IEnumerable<Inventory>> GetLowStock(int? customThreshold = null);
        public Task LoadFromJson(string path);
    }
}
