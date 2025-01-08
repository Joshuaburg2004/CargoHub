using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV2
{
    public class ItemGroupServiceV2 : IItemGroupServiceV2
    {
        private readonly CargoHubContext _cargoHubContext;


        public ItemGroupServiceV2(CargoHubContext context)
        {
            _cargoHubContext = context;
        }

        public async Task<ItemGroup?> FindItemGroup(int Id)
        {
            return await this._cargoHubContext.ItemGroups.FirstOrDefaultAsync(item_group => item_group.Id == Id);
        }
        public async Task<IEnumerable<ItemGroup?>> FindManyItemGroup(IEnumerable<int> Ids)
        {
            List<ItemGroup?> toReturn = new List<ItemGroup?>();

            foreach (int id in Ids)
            {
                toReturn.Append(await this._cargoHubContext.ItemGroups.FirstOrDefaultAsync(item_group => item_group.Id == id));
            }
            return toReturn;

        }
        public async Task<IEnumerable<ItemGroup>> GetAllItemGroup()
        {
            return await this._cargoHubContext.ItemGroups.ToListAsync();
        }

        public async Task<IEnumerable<ItemGroup>> GetAllItemGroup(int? pageIndex)
        {
            if (pageIndex == null)
            {
                return await GetAllItemGroup();
            }
            int page = (int)pageIndex;
            return await this._cargoHubContext.ItemGroups
                .OrderBy(c => c.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>?> GetItemsfromItemGroupById(int id)
        {
            if (id < 0) return null;
            return await this._cargoHubContext.Items.Where(_ => _.ItemGroup == id).ToListAsync();
        }


        public async Task<int?> AddItemGroup(ItemGroup linetype)
        {
            await _cargoHubContext.ItemGroups.AddAsync(linetype);
            await _cargoHubContext.SaveChangesAsync();
            return linetype.Id;
        }

        public async Task<ItemGroup?> UpdateItemGroup(int Id, ItemGroup toUpdate)
        {
            ItemGroup? found = await this.FindItemGroup(Id);
            if (found is null) return null;

            found.Name = toUpdate.Name;
            found.Description = toUpdate.Description;
            found.UpdatedAt = toUpdate.UpdatedAt;

            this._cargoHubContext.ItemGroups.Update(found);
            await this._cargoHubContext.SaveChangesAsync();
            return found;
        }


        public async Task<ItemGroup?> DeleteItemGroup(int Id)
        {
            ItemGroup? found = await this.FindItemGroup(Id);
            if (found is null) return null;

            this._cargoHubContext.ItemGroups.Remove(found);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
            else return null;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonItemGroup>? itemGroups = JsonSerializer.Deserialize<List<JsonItemGroup>>(json);
                if (itemGroups == null)
                {
                    return;
                }
                var transaction = _cargoHubContext.Database.BeginTransaction();
                foreach (JsonItemGroup jsonItemGroup in itemGroups)
                {
                    ItemGroup itemGroup = jsonItemGroup.ToItemGroup();
                    await SaveToDatabase(itemGroup);
                }
                await _cargoHubContext.SaveChangesAsync();
                transaction.Commit();
            }
        }
        public async Task<int> SaveToDatabase(ItemGroup itemGroup)
        {
            if (itemGroup is null)
            {
                return -1;
            }
            if (itemGroup.Name == null) { itemGroup.Name = "N/A"; }
            if (itemGroup.Description == null) { itemGroup.Description = "N/A"; }
            await _cargoHubContext.ItemGroups.AddAsync(itemGroup);
            return itemGroup.Id;
        }
    }
}