using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;

namespace CargoHubAlt.Services.ServicesV2
{
    public class ClientServiceV2 : IClientServiceV2
    {
        private readonly CargoHubContext _cargoHubContext;
        public ClientServiceV2(CargoHubContext context)
        {
            _cargoHubContext = context;
        }
        public async Task<List<Client>> GetAllClients()
        {
            return await _cargoHubContext.Clients.ToListAsync();
        }
        public async Task<Client?> GetClient(int id)
        {
            return await _cargoHubContext.Clients.FindAsync(id);
        }
        public async Task<int?> AddClient(Client client)
        {
            client.CreatedAt = Base.GetTimeStamp();
            client.UpdatedAt = Base.GetTimeStamp();
            await _cargoHubContext.Clients.AddAsync(client);
            await _cargoHubContext.SaveChangesAsync();
            return client.Id;
        }
        public async Task<string?> UpdateClient(int id, Client client)
        {
            Client? origClient = await GetClient(id);
            string changedFields = "";
            if (origClient == null)
            {
                return null;
            }
            if (origClient == null)
            {
                return null;
            }
            if (client.Name != origClient.Name)
            {
                origClient.Name = client.Name;
                changedFields += $"Name: {client.Name}, ";
            }
            if (client.Address != origClient.Address)
            {
                origClient.Address = client.Address;
                changedFields += $"Address: {client.Address}, ";
            }
            if (client.City != origClient.City)
            {
                origClient.City = client.City;
                changedFields += $"City: {client.City}, ";
            }
            if (client.ZipCode != origClient.ZipCode)
            {
                origClient.ZipCode = client.ZipCode;
                changedFields += $"ZipCode: {client.ZipCode}, ";
            }
            if (client.Province != origClient.Province)
            {
                origClient.Province = client.Province;
                changedFields += $"Province: {client.Province}, ";
            }
            if (client.Country != origClient.Country)
            {
                origClient.Country = client.Country;
                changedFields += $"Country: {client.Country}, ";
            }
            if (client.ContactName != origClient.ContactName)
            {
                origClient.ContactName = client.ContactName;
                changedFields += $"ContactName: {client.ContactName}, ";
            }
            if (client.ContactPhone != origClient.ContactPhone)
            {
                origClient.ContactPhone = client.ContactPhone;
                changedFields += $"ContactPhone: {client.ContactPhone}, ";
            }
            if (client.ContactEmail != origClient.ContactEmail)
            {
                origClient.ContactEmail = client.ContactEmail;
                changedFields += $"ContactEmail: {client.ContactEmail}, ";
            }
            origClient.UpdatedAt = Base.GetTimeStamp();
            await _cargoHubContext.SaveChangesAsync();
            return changedFields;
        }
        public async Task<Client?> RemoveClient(int id)
        {
            Client? client = _cargoHubContext.Clients.Find(id);
            if (client == null)
            {
                return client;
            }
            _cargoHubContext.Clients.Remove(client);
            await _cargoHubContext.SaveChangesAsync();
            return client;
        }
        public async Task<List<Order>> GetOrdersByClient(int id)
        {
            return await _cargoHubContext.Orders.Where(o => o.BillTo == id || o.ShipTo == id).ToListAsync();
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Client>? clients = JsonSerializer.Deserialize<List<Client>>(json);
                if (clients == null)
                {
                    return;
                }
                foreach (Client client in clients)
                {
                    await SaveToDatabase(client);
                }
            }
        }
        public async Task<int> SaveToDatabase(Client client)
        {
            if (client is null)
            {
                return -1;
            }
            if (client.Name == null) { client.Name = "N/A"; }
            if (client.Address == null) { client.Address = "N/A"; }
            if (client.City == null) { client.City = "N/A"; }
            if (client.ZipCode == null) { client.ZipCode = "N/A"; }
            if (client.Province == null) { client.Province = "N/A"; }
            if (client.Country == null) { client.Country = "N/A"; }
            if (client.ContactName == null) { client.ContactName = "N/A"; }
            if (client.ContactPhone == null) { client.ContactPhone = "N/A"; }
            if (client.ContactEmail == null) { client.ContactEmail = "N/A"; }
            await _cargoHubContext.Clients.AddAsync(client);
            await _cargoHubContext.SaveChangesAsync();
            return client.Id;
        }
    }
}
