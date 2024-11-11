using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class Suppliers : ISupplierService
    {
        private readonly CargoHubContext cargoHubContext;
        public Suppliers(CargoHubContext context)
        {
            cargoHubContext = context;
        }
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await cargoHubContext.Suppliers.ToListAsync();
        }
        public async Task<Supplier?> GetSupplier(int id)
        {
            return await cargoHubContext.Suppliers.FindAsync(id);
        }
        public async Task<int?> AddSupplier(Supplier supplier)
        {
            supplier.Created_At = Base.GetTimeStamp();
            supplier.Updated_At = Base.GetTimeStamp();
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
            origSupplier.Address_Extra = supplier.Address_Extra;
            origSupplier.City = supplier.City;
            origSupplier.Zip_Code = supplier.Zip_Code;
            origSupplier.Province = supplier.Province;
            origSupplier.Country = supplier.Country;
            origSupplier.Contact_Name = supplier.Contact_Name;
            origSupplier.PhoneNumber = supplier.PhoneNumber;
            origSupplier.Reference = supplier.Reference;
            origSupplier.Updated_At = Base.GetTimeStamp();

            cargoHubContext.Suppliers.Update(origSupplier);
            await cargoHubContext.SaveChangesAsync();
            return origSupplier;
        }
        public async Task<List<Item>?> GetItemsfromSupplierById(int id)
        {
            var items = await cargoHubContext.Items.Where(i => i.supplier_id == id).ToListAsync();
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
        public async Task SaveToDatabase(Supplier supplier) => await AddSupplier(supplier);
    }
}