public class WarehouseService : IWarehouseService
{
    private readonly CargoHubContext _context;

    public WarehouseService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<Warehouse> GetWarehouses() => await _context.Warehouses.ToListAsync();
}