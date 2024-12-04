using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface IItemLineServiceV1
    {
        public Task<ItemLine?> FindItemLine(int Id);
        public Task<IEnumerable<ItemLine?>> FindManyItemLine(IEnumerable<int> Ids);
        public Task<IEnumerable<ItemLine>> GetAllItemLine();
        public Task<IEnumerable<Item>?> GetItemsfromItemLineById(int id);
        public Task<int?> AddItemLine(ItemLine toAdd);
        public Task<ItemLine?> UpdateItemLine(int Id, ItemLine toUpdate);
        public Task<ItemLine?> DeleteItemLine(int Id);
        public Task LoadFromJson(string path);
    }
}
