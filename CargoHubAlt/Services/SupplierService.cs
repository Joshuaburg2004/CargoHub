using System.Text.Json;
using Microsoft.EntityFrameworkCore;
public class Suppliers : ISuppliers
{
    private readonly CargoHubContext cargoHubContext;
    public Suppliers(CargoHubContext context)
    {
        cargoHubContext = context;
    }
    public async Task<IEnumerable<Supplier>> GetSuppliers()
    {
        return await cargoHubContext.Suppliers.ToListAsync();
    }
    public async Task<Supplier?> GetOneSupplier(int id)
    {
        return await cargoHubContext.Suppliers.FindAsync(id);
    }
    public async Task<int?> CreateSupplier(Supplier supplier)
    {
        supplier.CreatedAt = Base.GetTimeStamp();
        supplier.UpdatedAt = Base.GetTimeStamp();
        cargoHubContext.Suppliers.Add(supplier);
        await cargoHubContext.SaveChangesAsync();
        return supplier.Id;
    }
    public async Task<Supplier?> DeleteSupplier(int id)
    {
        var supplier = await cargoHubContext.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return null;
        }
        cargoHubContext.Suppliers.Remove(supplier);
        await cargoHubContext.SaveChangesAsync();
        return supplier;
    }
    public async Task<Supplier?> UpdateSupplier(int id, Supplier supplier)
    {
        var origSupplier = await cargoHubContext.Suppliers.FindAsync(id);
        if (origSupplier == null) return default;

        origSupplier.Code = supplier.Code;
        origSupplier.Name = supplier.Name;
        origSupplier.Address = supplier.Address;
        origSupplier.AddressExtra = supplier.AddressExtra;
        origSupplier.City = supplier.City;
        origSupplier.ZipCode = supplier.ZipCode;
        origSupplier.Province = supplier.Province;
        origSupplier.Country = supplier.Country;
        origSupplier.ContactName = supplier.ContactName;
        origSupplier.PhoneNumber = supplier.PhoneNumber;
        origSupplier.Reference = supplier.Reference;
        origSupplier.UpdatedAt = Base.GetTimeStamp();

        cargoHubContext.Suppliers.Update(origSupplier);
        await cargoHubContext.SaveChangesAsync();
        return origSupplier;
    }
    public async Task<IEnumerable<Item>> GetItemsForSupplier(int id){
        var items = await cargoHubContext.Items.Where(i => i.SupplierId == id).ToListAsync();
        return items;
    }
    public async Task LoadFromJson(string path)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            List<Supplier>? suppliers = JsonSerializer.Deserialize<List<Supplier>>(json); // This wont work because the json is not correct with C# currently
            if (suppliers == null)
            {
                return;
            }
            foreach (Supplier supplier in suppliers)
            {
                await SaveToDatabase(supplier);
            }
        }
    }
    public async Task SaveToDatabase(Supplier supplier) => await CreateSupplier(supplier);
}