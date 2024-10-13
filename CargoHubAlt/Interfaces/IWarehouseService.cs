public interface IWarehouseService
{
    public Task<Warehouse> GetWarehouses();
    public Task<Warehouse> GetWarehousesById(Guid id);
    public Task<bool> CreateWarehouse(Warehouse warehouse);
    public Task<bool> UpdateWarehouse(Warehouse warehouse);
    public Task<bool> DeleteWarehouse(Guid id);
}