using System.Text.Json;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IEnumerable<Client>> GetBatchClients(int[] ids)
    {
        List<Client> clients = new List<Client>();
        foreach (var id in ids)
        {
            Client? client = await GetClient(id);
            if (client == null) { continue; }
            clients.Add(client);
        }
        return clients;
    }
    public async Task<Client?> GetClient(int id)
    {
        return await cargoHubContext.Clients.FindAsync(id);
    }
    public async Task<int?> AddClient(Client client)
    {
        await cargoHubContext.Clients.AddAsync(client);
        await cargoHubContext.SaveChangesAsync();
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
    public async Task LoadFromJson(string path)
    {
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
    public async Task SaveToDatabase(Client client) => await AddClient(client);
}