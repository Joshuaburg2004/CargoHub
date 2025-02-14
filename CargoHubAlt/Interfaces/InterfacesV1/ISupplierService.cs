using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface ISupplierServiceV1
    {
        public Task<Supplier?> GetSupplier(int Id);
        public Task<List<Supplier>> GetAllSuppliers();
        public Task<List<Item>?> GetItemsfromSupplierById(int Id);
        public Task<int?> AddSupplier(Supplier supplier);
        public Task<Supplier?> UpdateSupplier(int Id, Supplier supplier);
        public Task<Supplier?> DeleteSupplier(int Id);
        public Task LoadFromJson(string path);

    }
}
