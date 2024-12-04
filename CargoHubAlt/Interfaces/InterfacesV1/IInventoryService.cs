using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface IInventoryServiceV1
    {
        Task<int?> CreateInventory(Inventory inventory);
        Task<Inventory?> GetOneInventory(int id);
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory?> UpdateInventory(int id, Inventory inventory);
        Task<Inventory?> DeleteInventory(int id);
        public Task LoadFromJson(string path);
    }
}
