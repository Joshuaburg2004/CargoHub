using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class ClientService : IClientService
    {
        private readonly CargoHubContext _cargoHubContext;
        public ClientService(CargoHubContext context)
        {
            _cargoHubContext = context;
        }
        public async Task<IEnumerable<Client>> GetAllClients()
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
        public async Task<Client?> UpdateClient(int id, Client client)
        {
            Client? origClient = await GetClient(id);
            if (origClient == null)
            {
                return origClient;
            }
            origClient.Name = client.Name;
            origClient.Address = client.Address;
            origClient.City = client.City;
            origClient.ZipCode = client.ZipCode;
            origClient.Province = client.Province;
            origClient.Country = client.Country;
            origClient.ContactName = client.ContactName;
            origClient.ContactPhone = client.ContactPhone;
            origClient.ContactEmail = client.ContactEmail;
            origClient.UpdatedAt = Base.GetTimeStamp();
            await _cargoHubContext.SaveChangesAsync();
            return origClient;
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
            return await _cargoHubContext.Orders.Where(o => o.Bill_To == id || o.Ship_To == id).ToListAsync();
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
        public async Task<int> SaveToDatabase(Client client){
            if(client is null){
                return -1;
            }
            if(client.Name == null){client.Name = "N/A";}
            if(client.Address == null){client.Address = "N/A";}
            if(client.City == null){client.City = "N/A";}
            if(client.ZipCode == null){client.ZipCode = "N/A";}
            if(client.Province == null){client.Province = "N/A";}
            if(client.Country == null){client.Country = "N/A";}
            if(client.ContactName == null){client.ContactName = "N/A";}
            if(client.ContactPhone == null){client.ContactPhone = "N/A";}
            if(client.ContactEmail == null){client.ContactEmail = "N/A";}
            await _cargoHubContext.Clients.AddAsync(client);
            await _cargoHubContext.SaveChangesAsync();
            return client.Id;
        }
    }
}
