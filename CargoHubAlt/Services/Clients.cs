using System.Text.Json;
using Microsoft.EntityFrameworkCore;
public class Clients : IClients
{
    private readonly CargoHubContext cargoHubContext;
    public Clients(CargoHubContext context)
    {
        cargoHubContext = context;
    }
    public async Task<IEnumerable<Client>> GetAllClients()
    {
        return await cargoHubContext.Clients.ToListAsync();
    }
    public async Task<IEnumerable<Client>> GetBatchClients(Guid[] guids)
    {
        List<Client> clients = new List<Client>();
        foreach (var guid in guids)
        {
            Client? client = await GetClient(guid);
            if (client == null) { continue; }
            clients.Add(client);
        }
        return clients;
    }
    public async Task<Client?> GetClient(Guid guid)
    {
        return await cargoHubContext.Clients.FindAsync(guid);
    }
    public async Task<Guid?> AddClient(Client client)
    {
        await cargoHubContext.Clients.AddAsync(client);
        await cargoHubContext.SaveChangesAsync();
        return client.Id;
    }
    public async Task<Client?> UpdateClient(Guid guid, Client client)
    {
        Client? origClient = await GetClient(guid);
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
    public async Task<Client?> RemoveClient(Guid guid)
    {
        Client? client = cargoHubContext.Clients.Find(guid);
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