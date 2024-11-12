using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;
using System.Text.Json;

namespace CargoHubAlt.Services
{
    public class ItemGroupService : IItemGroupService
    {
        private readonly CargoHubContext _cargoHubContext;


        public ItemGroupService(CargoHubContext context)
        {
            _cargoHubContext = context;
        }

        public async Task<ItemGroup?> FindItemGroup(int Id)
        {
            return await this._cargoHubContext.ItemGroups.FirstOrDefaultAsync(item_group => item_group.id == Id);
        }
        public async Task<IEnumerable<ItemGroup?>> FindManyItemGroup(IEnumerable<int> Ids)
        {
            List<ItemGroup?> toReturn = new List<ItemGroup?>();

            foreach (int id in Ids)
            {
                toReturn.Append(await this._cargoHubContext.ItemGroups.FirstOrDefaultAsync(item_group => item_group.id == id));
            }
            return toReturn;

        }
        public async Task<IEnumerable<ItemGroup>> GetAllItemGroup()
        {
            return await this._cargoHubContext.ItemGroups.ToListAsync();
        }

        public async Task<IEnumerable<Item>?> GetItemsfromItemGroupById(int id)
        {
            if (id < 0) return null;
            return await this._cargoHubContext.Items.Where(_ => _.item_group == id).ToListAsync();
        }


        public async Task<int?> AddItemGroup(ItemGroup linetype)
        {
            await _cargoHubContext.ItemGroups.AddAsync(linetype);
            await _cargoHubContext.SaveChangesAsync();
            return linetype.id;
        }

        public async Task<ItemGroup?> UpdateItemGroup(int Id, ItemGroup toUpdate)
        {
            ItemGroup? found = await this.FindItemGroup(Id);
            if (found is null) return null;

            found.name = toUpdate.name;
            found.description = toUpdate.description;
            found.updated_at = toUpdate.updated_at;
            
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
                List<ItemGroup>? inventorys = JsonSerializer.Deserialize<List<ItemGroup>>(json);
                if (inventorys == null)
                {
                    return;
                }
                foreach (ItemGroup inventory in inventorys)
                {
                    await SaveToDatabase(inventory);
                }
            }
        }
        public async Task<int> SaveToDatabase(ItemGroup itemGroup){
            if(itemGroup is null){
                return -1;
            }
            if(itemGroup.name == null){itemGroup.name = "N/A";}
            if(itemGroup.description == null){itemGroup.description = "N/A";}
            await _cargoHubContext.ItemGroups.AddAsync(itemGroup);
            await _cargoHubContext.SaveChangesAsync();
            return itemGroup.id;   
        }
    }
}