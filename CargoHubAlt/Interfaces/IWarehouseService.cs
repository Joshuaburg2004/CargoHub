using CargoHubAlt.Models;

public interface IWarehouse
{
    public Task<List<Warehouse>> GetWarehouses();
    public Task<Warehouse> GetWarehousesById(int id);
    public Task<bool> AddWarehouse(Warehouse warehouse);
    public Task<bool> UpdateWarehouse(Warehouse warehouse);
    public Task<bool> DeleteWarehouse(int id);
}