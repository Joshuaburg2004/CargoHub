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

    public async Task<Warehouse> GetWarehousesById(Guid id) => await _context.Warehouses.FindAsync(id);

    public async Task<bool> AddWarehouse(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateWarehouse(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteWarehouse(Guid id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }
}