using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface IItemsServiceV1
    {
        public Task<Item?> GetItem(string id);
        public Task<List<Item>> GetItems();
        public Task<List<Inventory>> GetInventoryByItem(string id);
        public Task<Dictionary<string, int>> GetInventoryTotalsByItem(string id);
        public Task<string?> AddItem(Item item);
        public Task<Item?> UpdateItem(string toUpdate, Item UpdateTo);
        public Task<Item?> RemoveItem(string toRemove);
        public Task LoadFromJson(string path);
    }
}
