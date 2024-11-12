using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;

namespace CargoHubAlt.Services
{
    public class ClientService : IClientService
    {
        private readonly CargoHubContext cargoHubContext;
        public ClientService(CargoHubContext context)
        {
            cargoHubContext = context;
        }
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await cargoHubContext.Clients.ToListAsync();
        }
        public async Task<Client?> GetClient(int id)
        {
            return await cargoHubContext.Clients.FindAsync(id);
        }
        public async Task<int?> AddClient(Client client)
        {
            client.created_at = Base.GetTimeStamp();
            client.updated_at = Base.GetTimeStamp();
            await cargoHubContext.Clients.AddAsync(client);
            await cargoHubContext.SaveChangesAsync();
            return client.id;
        }
        public async Task<Client?> UpdateClient(int id, Client client)
        {
            Client? origClient = await GetClient(id);
            if (origClient == null)
            {
                return origClient;
            }
            origClient.name = client.name;
            origClient.address = client.address;
            origClient.city = client.city;
            origClient.zip_code = client.zip_code;
            origClient.province = client.province;
            origClient.country = client.country;
            origClient.contact_name = client.contact_name;
            origClient.contact_phone = client.contact_phone;
            origClient.contact_email = client.contact_email;
            origClient.updated_at = Base.GetTimeStamp();
            await cargoHubContext.SaveChangesAsync();
            return origClient;
        }
        public async Task<Client?> RemoveClient(int id)
        {
            Client? client = cargoHubContext.Clients.Find(id);
            if (client == null)
            {
                return client;
            }
            cargoHubContext.Clients.Remove(client);
            await cargoHubContext.SaveChangesAsync();
            return client;
        }
        public async Task<List<Order>> GetOrdersByClient(int id)
        {
            return await cargoHubContext.Orders.Where(o => o.Bill_To == id || o.Ship_To == id).ToListAsync();
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
            if(client.name == null){client.name = "N/A";}
            if(client.address == null){client.address = "N/A";}
            if(client.city == null){client.city = "N/A";}
            if(client.zip_code == null){client.zip_code = "N/A";}
            if(client.province == null){client.province = "N/A";}
            if(client.country == null){client.country = "N/A";}
            if(client.contact_name == null){client.contact_name = "N/A";}
            if(client.contact_phone == null){client.contact_phone = "N/A";}
            if(client.contact_email == null){client.contact_email = "N/A";}
            await cargoHubContext.Clients.AddAsync(client);
            await cargoHubContext.SaveChangesAsync();
            return client.id;   
        }
    }
}
