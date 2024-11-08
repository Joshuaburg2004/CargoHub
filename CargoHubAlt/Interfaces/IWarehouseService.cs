using CargoHubAlt.Models;

public interface IWarehouse
{
    public Task<List<Warehouse>> GetWarehouses();
    public Task<Warehouse> GetWarehousesById(int id);
    public Task<int?> AddWarehouse(Warehouse warehouse);
    public Task<Warehouse> UpdateWarehouse(int id, Warehouse warehouse);
    public Task<bool> DeleteWarehouse(int id);
    public Task<List<Location>> GetLocationsByWarehouse(int id);
}