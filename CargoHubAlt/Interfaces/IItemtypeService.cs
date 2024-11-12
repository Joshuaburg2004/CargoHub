using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IItemTypeService
    {
        public Task<Item_type?> GetItemTypeById(int Id);
        public Task<IEnumerable<Item_type>> GetAllItemType();
        public Task<IEnumerable<Item>?> GetItemsfromItemTypeById(int Id);
        public Task<int?> AddItemType(Item_type itemline);
        public Task<Item_type?> UpdateItemType(int Id, Item_type itemline);
        public Task<Item_type?> DeleteItemType(int Id);
    }
}
