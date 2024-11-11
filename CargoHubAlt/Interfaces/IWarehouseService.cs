using CargoHubAlt.Models;
namespace CargoHubAlt.Interfaces
{
    public interface IWarehouseService
    {
        public Task<Warehouse?> GetWarehouse(int Id);
        public Task<IEnumerable<Warehouse>> GetAllWarehouse();
        public Task<IEnumerable<Location>?> GetLocationsfromWarehouseById(int Id);
        public Task<int?> AddWarehouse(Warehouse warehouse);
        public Task<Warehouse?> UpdateWarehouse(int Id, Warehouse warehouse);
        public Task<Warehouse?> DeleteWarehouse(int Id);
    }
}
