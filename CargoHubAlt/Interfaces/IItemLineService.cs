using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IItemLineService
    {
        public Task<Item_line?> FindItemLine(int Id);
        public Task<IEnumerable<Item_line?>> FindManyItemLine(IEnumerable<int> Ids);
        public Task<IEnumerable<Item_line>> GetAllItemLine();
        public Task<IEnumerable<Item>?> GetItemsfromItemLineById(int id);
        public Task<int?> AddItemLine(Item_line toAdd);
        public Task<Item_line?> UpdateItemLine(int Id, Item_line toUpdate);
        public Task<Item_line?> DeleteItemLine(int Id);
    }
}
