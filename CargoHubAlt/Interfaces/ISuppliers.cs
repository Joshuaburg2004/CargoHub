public interface ISuppliers{
    public Task<IEnumerable<Supplier>> GetAllSuppliers();
    public Task<IEnumerable<Supplier>> GetBatchSuppliers(Guid[] ids);
    public Task<Supplier?> GetOneSupplier(Guid id);
    public Task<Guid?> CreateSupplier(Supplier supplier);
    public Task<Supplier?> DeleteSupplier(Guid id);
    public Task<Supplier?> UpdateSupplier(Guid id, Supplier supplier);
    // commented out because Item is not done yet
    // public Task<IEnumerable<Item>?> GetItemsForSupplier(Guid id);
}