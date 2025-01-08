using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV2
{
    public class ItemTypeServiceV2 : IItemTypeServiceV2
    {
        private readonly CargoHubContext _cargoHubContext;


        public ItemTypeServiceV2(CargoHubContext context)
        {
            _cargoHubContext = context;
        }

        public async Task<ItemType?> GetItemTypeById(int Id)
        {

            return await _cargoHubContext.ItemTypes.FirstOrDefaultAsync(item_type => item_type.Id == Id);
        }

        public async Task<IEnumerable<ItemType>> GetAllItemType()
        {
            return await _cargoHubContext.ItemTypes.ToListAsync();
        }

        public async Task<IEnumerable<ItemType>> GetAllItemType(int? pageIndex)
        {
            if (pageIndex == null)
            {
                return await GetAllItemType();
            }
            int page = (int)pageIndex;
            var itemTypes = await _cargoHubContext.ItemTypes
                .OrderBy(c => c.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
            return itemTypes;
        }

        public async Task<IEnumerable<Item>?> GetItemsfromItemTypeById(int Id)
        {
            if (Id < 0) return null;
            List<Item> toReturn = await _cargoHubContext.Items.Where(_ => _.ItemType == Id).ToListAsync();
            return toReturn;
        }

        public async Task<int?> AddItemType(ItemType itemtype)
        {
            ItemType? found = await _cargoHubContext.ItemTypes.FirstOrDefaultAsync(x => x.Id == itemtype.Id);
            if (found is not null) { return null; }

            await _cargoHubContext.ItemTypes.AddAsync(itemtype);
            await _cargoHubContext.SaveChangesAsync();
            return itemtype.Id;
        }

        public async Task<ItemType?> UpdateItemType(int Id, ItemType itemtype)
        {
            ItemType? found = await GetItemTypeById(Id);
            if (found is null) return null;

            found.Name = itemtype.Name;
            found.Description = itemtype.Description;
            found.UpdatedAt = itemtype.UpdatedAt;

            _cargoHubContext.ItemTypes.Update(found);
            await _cargoHubContext.SaveChangesAsync();
            return found;
        }


        public async Task<ItemType?> DeleteItemType(int Id)
        {
            ItemType? found = await GetItemTypeById(Id);
            if (found is null) return null;

            _cargoHubContext.ItemTypes.Remove(found);
            await _cargoHubContext.SaveChangesAsync();
            return found;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonItemType>? itemTypes = JsonSerializer.Deserialize<List<JsonItemType>>(json);
                if (itemTypes == null)
                {
                    return;
                }
                var transaction = _cargoHubContext.Database.BeginTransaction();
                foreach (JsonItemType jsonItemType in itemTypes)
                {
                    ItemType itemType = jsonItemType.ToItemType();
                    await SaveToDatabase(itemType);
                }
                await _cargoHubContext.SaveChangesAsync();
                transaction.Commit();
            }
        }
        public async Task<int> SaveToDatabase(ItemType itemType)
        {
            if (itemType is null)
            {
                return -1;
            }
            if (itemType.Name == null) { itemType.Name = "N/A"; }
            if (itemType.Description == null) { itemType.Description = "N/A"; }
            await _cargoHubContext.ItemTypes.AddAsync(itemType);
            return itemType.Id;
        }
    }
}