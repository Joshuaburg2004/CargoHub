using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface IItemTypeServiceV1
    {
        public Task<ItemType?> GetItemTypeById(int Id);
        public Task<IEnumerable<ItemType>> GetAllItemType();
        public Task<IEnumerable<Item>?> GetItemsfromItemTypeById(int Id);
        public Task<int?> AddItemType(ItemType itemline);
        public Task<ItemType?> UpdateItemType(int Id, ItemType itemline);
        public Task<ItemType?> DeleteItemType(int Id);
        public Task LoadFromJson(string path);
    }
}
