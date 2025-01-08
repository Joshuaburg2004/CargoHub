using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV2
{
    public class ItemLineServiceV2 : IItemLineServiceV2
    {
        private readonly CargoHubContext _cargoHubContext;

        public ItemLineServiceV2(CargoHubContext context)
        {
            _cargoHubContext = context;
        }

        public async Task<ItemLine?> FindItemLine(int Id)
        {
            return await this._cargoHubContext.ItemLines.FirstOrDefaultAsync(item_Line => item_Line.Id == Id);
        }

        public async Task<IEnumerable<ItemLine?>> FindManyItemLine(IEnumerable<int> Ids)
        {
            List<ItemLine?> toReturn = new List<ItemLine?>();

            foreach (int id in Ids)
            {
                toReturn.Append(await this._cargoHubContext.ItemLines.FirstOrDefaultAsync(item_Line => item_Line.Id == id));
            }
            return toReturn;

        }

        public async Task<IEnumerable<Item>?> GetItemsfromItemLineById(int Id)
        {
            if (Id < 0) return null;
            List<Item> toReturn = await _cargoHubContext.Items.Where(_ => _.ItemLine == Id).ToListAsync();
            return toReturn;
        }

        public async Task<IEnumerable<ItemLine>> GetAllItemLine()
        {
            return await _cargoHubContext.ItemLines.ToListAsync();
        }

        public async Task<IEnumerable<ItemLine>> GetAllItemLine(int? pageIndex)
        {
            if (pageIndex == null)
            {
                return await GetAllItemLine();
            }
            int page = (int)pageIndex;
            var itemLines = await _cargoHubContext.ItemLines
                .OrderBy(c => c.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
            return itemLines;
        }

        public async Task<int?> AddItemLine(ItemLine linetype)
        {
            await _cargoHubContext.ItemLines.AddAsync(linetype);
            await _cargoHubContext.SaveChangesAsync();
            return linetype.Id;
        }

        public async Task<ItemLine?> UpdateItemLine(int Id, ItemLine toUpdate)
        {
            ItemLine? found = await _cargoHubContext.ItemLines.FirstOrDefaultAsync(item_Line => item_Line.Id == Id);
            if (found == null) return found;

            found.Name = toUpdate.Name;
            found.Description = toUpdate.Description;
            found.UpdatedAt = Base.GetTimeStamp();

            this._cargoHubContext.ItemLines.Update(found);
            await this._cargoHubContext.SaveChangesAsync();
            return found;
        }

        public async Task<ItemLine?> DeleteItemLine(int Id)
        {
            ItemLine? found = await this.FindItemLine(Id);
            if (found is null) return null;

            this._cargoHubContext.ItemLines.Remove(found);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
            else return null;
        }

        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<ItemLine>? itemLines = JsonSerializer.Deserialize<List<ItemLine>>(json);
                if (itemLines == null)
                {
                    return;
                }
                foreach (ItemLine inventory in itemLines)
                {
                    await SaveToDatabase(inventory);
                }
            }
        }

        public async Task<int> SaveToDatabase(ItemLine itemGroup)
        {
            if (itemGroup is null)
            {
                return -1;
            }
            if (itemGroup.Name == null) { itemGroup.Name = "N/A"; }
            if (itemGroup.Description == null) { itemGroup.Description = "N/A"; }
            await _cargoHubContext.ItemLines.AddAsync(itemGroup);
            await _cargoHubContext.SaveChangesAsync();
            return itemGroup.Id;
        }
    }
}