using CargoHubAlt.Models;
using Microsoft.EntityFrameworkCore;

public class WarehouseService : IWarehouse
{
    private readonly CargoHubContext _context;

    public WarehouseService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<List<Warehouse?>> GetWarehouses() => await _context.Warehouses.ToListAsync();

    public async Task<Warehouse?> GetWarehousesById(int id) => await _context.Warehouses.FirstOrDefaultAsync(_ => _.Id == id);

    public async Task<int?> AddWarehouse(Warehouse warehouse)
    {
        if (await _context.Warehouses.FindAsync(warehouse.Id) != null)
            return null;
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

        await _context.SaveChangesAsync();
        return existingWarehouse;
    }

    public async Task<bool> DeleteWarehouse(int id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Location>> GetLocationsByWarehouse(int id) => await _context.Locations.Where(l => l.Warehouse_Id == id).ToListAsync();
}