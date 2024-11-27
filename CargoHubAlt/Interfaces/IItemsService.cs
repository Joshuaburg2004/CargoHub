using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IItemsService
    {
        public Task<Item?> GetItem(string id);
        public Task<IEnumerable<Item>> GetItems();
        public Task<IEnumerable<Inventory>> GetInventoryByItem(string id);
        public Task<Dictionary<string, int>> GetInventoryTotalsByItem(string id);
        public Task<string?> AddItem(Item item);
        public Task<Item?> UpdateItem(string toUpdate, Item UpdateTo);
        public Task<Item?> RemoveItem(string toRemove);
        public Task LoadFromJson(string path);
    }
}
