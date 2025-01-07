using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IInventoryServiceV2
    {
        Task<int?> CreateInventory(Inventory inventory);
        Task<Inventory?> GetOneInventory(int id);
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<IEnumerable<Inventory>> GetAllInventories(int? pageIndex);
        Task<Inventory?> UpdateInventory(int id, Inventory inventory);
        Task<Inventory?> DeleteInventory(int id);
        public Task LoadFromJson(string path);
    }
}
