using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IItemGroupService
    {
        public Task<ItemGroup?> FindItemGroup(int Id);
        public Task<IEnumerable<ItemGroup?>> FindManyItemGroup(IEnumerable<int> Ids);
        public Task<IEnumerable<ItemGroup>> GetAllItemGroup();
        public Task<IEnumerable<Item>?> GetItemsfromItemGroupById(int id);

        public Task<int?> AddItemGroup(ItemGroup toAdd);
        public Task<ItemGroup?> UpdateItemGroup(int Id, ItemGroup toUpdate);
        public Task<ItemGroup?> DeleteItemGroup(int Id);
        public Task LoadFromJson(string path);
    }
}
