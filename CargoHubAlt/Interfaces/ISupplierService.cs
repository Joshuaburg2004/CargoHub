public interface ISuppliers{
    public Task<IEnumerable<Supplier>> GetSuppliers();
    public Task<Supplier?> GetSupplier(int id);
    public Task<IEnumerable<Item>?> GetItemsForSupplier(int id);
    public Task<int?> CreateSupplier(Supplier supplier);
    public Task<Supplier?> DeleteSupplier(int id);
    public Task<Supplier?> UpdateSupplier(int id, Supplier supplier);
}
