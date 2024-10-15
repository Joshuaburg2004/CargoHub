using CargoHubAlt.Models;

public interface IWarehouseService
{
    public Task<List<Warehouse>> GetWarehouses();
    public Task<Warehouse> GetWarehousesById(Guid id);
    public Task<bool> AddWarehouse(Warehouse warehouse);
    public Task<bool> UpdateWarehouse(Warehouse warehouse);
    public Task<bool> DeleteWarehouse(Guid id);
}