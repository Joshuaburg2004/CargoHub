using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;

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
                List<JsonItemLine>? itemLines = JsonSerializer.Deserialize<List<JsonItemLine>>(json);
                if (itemLines == null)
                {
                    return;
                }
                var transaction = _cargoHubContext.Database.BeginTransaction();
                foreach (JsonItemLine jsonItemLine in itemLines)
                {
                    ItemLine itemLine = jsonItemLine.ToItemLine();
                    await SaveToDatabase(itemLine);
                }
                await _cargoHubContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<int> SaveToDatabase(ItemLine itemLine)
        {
            if (itemLine is null)
            {
                return -1;
            }
            if (itemLine.Name == null) { itemLine.Name = "N/A"; }
            if (itemLine.Description == null) { itemLine.Description = "N/A"; }
            await _cargoHubContext.ItemLines.AddAsync(itemLine);
            return itemLine.Id;
        }
    }
}