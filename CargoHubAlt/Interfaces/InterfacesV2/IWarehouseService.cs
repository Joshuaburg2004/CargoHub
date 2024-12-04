using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IWarehouseServiceV2
    {
        public Task<Warehouse?> GetWarehouseById(int Id);
        public Task<List<Warehouse>?> GetAllWarehouses();
        public Task<List<Location>?> GetLocationsfromWarehouseById(int Id);
        public Task<int?> AddWarehouse(Warehouse warehouse);
        public Task<Warehouse?> UpdateWarehouse(int Id, Warehouse warehouse);
        public Task<Warehouse?> DeleteWarehouse(int Id);
        public Task LoadFromJson(string path);
    }
}
