using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IInventoryServiceV2
    {
        Task<int?> CreateInventory(Inventory inventory);
        Task<Inventory?> GetOneInventory(int id);
        Task<List<Inventory>> GetAllInventories();
        Task<List<Inventory>> GetAllInventories(int? pageIndex);
        Task<Inventory?> UpdateInventory(int id, Inventory inventory);
        Task<Inventory?> DeleteInventory(int id);
        Task<List<Inventory>> GetLowStock(int? customThreshold = null);
        public Task LoadFromJson(string path);
    }
}
