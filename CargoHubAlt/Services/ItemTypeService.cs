using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class ItemTypeService : IItemTypeService
    {
        private readonly CargoHubContext _cargoHubContext;


        public ItemTypeService(CargoHubContext context)
        {
            _cargoHubContext = context;
        }

        public async Task<Item_type?> GetItemTypeById(int Id)
        {

            return await _cargoHubContext.Item_Types.FirstOrDefaultAsync(item_type => item_type.Id == Id);
        }

        public async Task<IEnumerable<Item_type>> GetAllItemType()
        {
            return await _cargoHubContext.Item_Types.ToListAsync();
        }


        public async Task<IEnumerable<Item>?> GetItemsfromItemTypeById(int Id)
        {
            if (Id < 0) return null;
            List<Item> toReturn = await _cargoHubContext.Items.Where(_ => _.item_type == Id).ToListAsync();
            return toReturn;
        }

        public async Task<int?> AddItemType(Item_type itemtype)
        {
            Item_type? found = await _cargoHubContext.Item_Types.FirstOrDefaultAsync(x => x.Id == itemtype.Id);
            if (found is not null) { return null; }

            await _cargoHubContext.Item_Types.AddAsync(itemtype);
            await _cargoHubContext.SaveChangesAsync();
            return itemtype.Id;
        }

        public async Task<Item_type?> UpdateItemType(int Id, Item_type itemtype)
        {
            Item_type? found = await GetItemTypeById(Id);
            if (found is null) return null;

        found.Name = itemtype.Name;
        found.Description = itemtype.Description;
        found.Updated_At = itemtype.Updated_At;

            _cargoHubContext.Item_Types.Update(found);
            await _cargoHubContext.SaveChangesAsync();
            return found;
        }


        public async Task<Item_type?> DeleteItemType(int Id)
        {
            Item_type? found = await GetItemTypeById(Id);
            if (found is null) return null;

            _cargoHubContext.Item_Types.Remove(found);
            await _cargoHubContext.SaveChangesAsync();
            return found;
        }
    }
}