using CargoHubAlt.Models;
using Microsoft.EntityFrameworkCore;

public class WarehouseService : IWarehouse
{
    private readonly CargoHubContext _context;

    public WarehouseService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<List<Warehouse>> GetWarehouses() => await _context.Warehouses.ToListAsync();

    public async Task<Warehouse> GetWarehousesById(int id) => await _context.Warehouses.FindAsync(id);

    public async Task<bool> AddWarehouse(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateWarehouse(Warehouse warehouse)
    {
        if (warehouse == null)
            return false;
        var existingWarehouse = await _context.Warehouses.FindAsync(warehouse.Id);
        if (existingWarehouse == null)
            return false;

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
        return true;
    }

    public async Task<bool> DeleteWarehouse(int id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }
}