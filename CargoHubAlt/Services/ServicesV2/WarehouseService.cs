using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV2
{
    public class WarehouseServiceV2 : IWarehouseServiceV2
    {
        private readonly CargoHubContext _context;

        public WarehouseServiceV2(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>?> GetAllWarehouses() => await _context.Warehouses.ToListAsync();
        public async Task<List<Warehouse>?> GetAllWarehouses(int? pageIndex)
        {
            if (pageIndex == null)
                return await GetAllWarehouses();
            int page = (int)pageIndex;
            return await _context.Warehouses
                .OrderBy(w => w.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseById(int id) => await _context.Warehouses.FirstOrDefaultAsync(_ => _.Id == id);

        public async Task<int?> AddWarehouse(Warehouse warehouse)
        {
            if (await _context.Warehouses.FindAsync(warehouse.Id) != null)
                return null;
            warehouse.CreatedAt = Base.GetTimeStamp();
            warehouse.UpdatedAt = Base.GetTimeStamp();
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse.Id;
        }


        public async Task<Warehouse?> UpdateWarehouse(int id, Warehouse warehouse)
        {
            if (warehouse == null)
                return warehouse;
            var existingWarehouse = await _context.Warehouses.FindAsync(id);
            if (existingWarehouse == null)
                return null;

            existingWarehouse.Id = warehouse.Id;
            existingWarehouse.Code = warehouse.Code;
            existingWarehouse.Name = warehouse.Name;
            existingWarehouse.Address = warehouse.Address;
            existingWarehouse.Zip = warehouse.Zip;
            existingWarehouse.City = warehouse.City;
            existingWarehouse.Province = warehouse.Province;
            existingWarehouse.Country = warehouse.Country;
            existingWarehouse.Contact = warehouse.Contact;
            existingWarehouse.UpdatedAt = Base.GetTimeStamp();

            await _context.SaveChangesAsync();
            return existingWarehouse;
        }

        public async Task<Warehouse?> DeleteWarehouse(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return null;
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<List<Location>?> GetLocationsfromWarehouseById(int id) => await _context.Locations.Where(l => l.WarehouseId == id).ToListAsync();
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Warehouse>? warehouses = JsonSerializer.Deserialize<List<Warehouse>>(json);
                if (warehouses == null)
                {
                    return;
                }
                foreach (Warehouse warehouse in warehouses)
                {
                    await SaveToDatabase(warehouse);
                }
            }
        }
        public async Task<int> SaveToDatabase(Warehouse warehouse)
        {
            if (warehouse is null)
            {
                return -1;
            }
            if (warehouse.Code == null) { warehouse.Code = "N/A"; }
            if (warehouse.Name == null) { warehouse.Name = "N/A"; }
            if (warehouse.Address == null) { warehouse.Address = "N/A"; }
            if (warehouse.Zip == null) { warehouse.Zip = "N/A"; }
            if (warehouse.Province == null) { warehouse.Province = "N/A"; }
            if (warehouse.Country == null) { warehouse.Country = "N/A"; }
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return warehouse.Id;
        }
    }

}