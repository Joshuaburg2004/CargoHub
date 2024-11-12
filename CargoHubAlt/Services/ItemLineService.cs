using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class ItemLineService : IItemLineService
    {
        private readonly CargoHubContext _cargoHubContext;

        public ItemLineService(CargoHubContext context)
        {
            _cargoHubContext = context;
        }

        public async Task<Item_line?> FindItemLine(int Id)
        {
            return await this._cargoHubContext.ItemLines.FirstOrDefaultAsync(item_Line => item_Line.Id == Id);
        }

        public async Task<IEnumerable<Item_line?>> FindManyItemLine(IEnumerable<int> Ids)
        {
            List<Item_line?> toReturn = new List<Item_line?>();

            foreach (int id in Ids)
            {
                toReturn.Append(await this._cargoHubContext.ItemLines.FirstOrDefaultAsync(item_Line => item_Line.Id == id));
            }
            return toReturn;

        }

        public async Task<IEnumerable<Item>?> GetItemsfromItemLineById(int Id)
        {
            if (Id < 0) return null;
            List<Item> toReturn = await _cargoHubContext.Items.Where(_ => _.item_line == Id).ToListAsync();
            return toReturn;
        }

        public async Task<IEnumerable<Item_line>> GetAllItemLine()
        {
            return await _cargoHubContext.ItemLines.ToListAsync();
        }
        public async Task<int?> AddItemLine(Item_line linetype)
        {
            await _cargoHubContext.ItemLines.AddAsync(linetype);
            await _cargoHubContext.SaveChangesAsync();
            return linetype.Id;
        }
        public async Task<Item_line?> UpdateItemLine(int Id, Item_line toUpdate)
        {
            Item_line? found = await _cargoHubContext.ItemLines.FirstOrDefaultAsync(item_Line => item_Line.Id == Id);
            if (found == null) return found;

        found.Name = toUpdate.Name;
        found.Description = toUpdate.Description;
        found.Updated_At = Base.GetTimeStamp();

            this._cargoHubContext.ItemLines.Update(found);
            await this._cargoHubContext.SaveChangesAsync();
            return found;
        }


        public async Task<Item_line?> DeleteItemLine(int Id)
        {
            Item_line? found = await this.FindItemLine(Id);
            if (found is null) return null;

            this._cargoHubContext.ItemLines.Remove(found);
            if (await this._cargoHubContext.SaveChangesAsync() >= 1) return found;
            else return null;
        }
    }
}