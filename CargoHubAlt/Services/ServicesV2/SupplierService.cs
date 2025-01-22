using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV2
{
    public class SupplierServiceV2 : ISupplierServiceV2
    {
        private readonly CargoHubContext cargoHubContext;
        public SupplierServiceV2(CargoHubContext context)
        {
            cargoHubContext = context;
        }
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await cargoHubContext.Suppliers.ToListAsync();
        }
        public async Task<List<Supplier>> GetAllSuppliers(int? pageIndex)
        {
            if(pageIndex == null)
            {
                return await GetAllSuppliers();
            }
            int page = (int)pageIndex;
            var suppliers = await cargoHubContext.Suppliers
                .OrderBy(s => s.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
            return suppliers;
        }
        public async Task<Supplier?> GetSupplier(int id)
        {
            return await cargoHubContext.Suppliers.FindAsync(id);
        }
        public async Task<int?> AddSupplier(Supplier supplier)
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
            origSupplier.Phonenumber = supplier.Phonenumber;
            origSupplier.Reference = supplier.Reference;
            origSupplier.UpdatedAt = Base.GetTimeStamp();

            cargoHubContext.Suppliers.Update(origSupplier);
            await cargoHubContext.SaveChangesAsync();
            return origSupplier;
        }
        public async Task<List<Item>?> GetItemsfromSupplierById(int id)
        {
            var items = await cargoHubContext.Items.Where(i => i.SupplierId == id).ToListAsync();
            return items;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonSupplier>? suppliers = JsonSerializer.Deserialize<List<JsonSupplier>>(json);
                if (suppliers == null)
                {
                    return;
                }
                var transaction = cargoHubContext.Database.BeginTransaction();
                foreach (JsonSupplier jsonSupplier in suppliers)
                {
                    Supplier supplier = jsonSupplier.ToSupplier();
                    await SaveToDatabase(supplier);
                }
                await cargoHubContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<int> SaveToDatabase(Supplier supplier)
        {
            if (supplier is null)
            {
                return -1;
            }
            if (supplier.Name == null) { supplier.Name = "N/A"; }
            if (supplier.Address == null) { supplier.Address = "N/A"; }
            if (supplier.City == null) { supplier.City = "N/A"; }
            if (supplier.AddressExtra == null) { supplier.AddressExtra = "N/A"; }
            if (supplier.Province == null) { supplier.Province = "N/A"; }
            if (supplier.Country == null) { supplier.Country = "N/A"; }
            if (supplier.Code == null) { supplier.Code = "N/A"; }
            if (supplier.ZipCode == null) { supplier.ZipCode = "N/A"; }
            if (supplier.ContactName == null) { supplier.ContactName = "N/A"; }
            if (supplier.Phonenumber == null) { supplier.Phonenumber = "N/A"; }
            if (supplier.Reference == null) { supplier.Reference = "N/A"; }
            await cargoHubContext.Suppliers.AddAsync(supplier);
            return supplier.Id;
        }
    }
}