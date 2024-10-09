using System.Text.Json;
public class Clients : IClients{
    private readonly CargoHubContext cargoHubContext;
    public Clients(CargoHubContext context){
        cargoHubContext = context;
    }
    public async Task<IEnumerable<Client>> GetAllClients(){
        return cargoHubContext.Clients;
    }
    public async Task<IEnumerable<Client>> GetBatchClients(Guid[] guids){
        return cargoHubContext.Clients.Where(client => guids.Contains(client.Id));
    }
    public async Task<Client?> GetClient(Guid guid){
        return cargoHubContext.Clients.FindAsync(guid).Result;
    }
    public async Task AddClient(Client client){
        await cargoHubContext.Clients.AddAsync(client);
        cargoHubContext.SaveChanges();
    }
    public async Task UpdateClient(Client client){
        client.UpdatedAt = Base.GetTimeStamp();
        cargoHubContext.Clients.Update(client);
        await cargoHubContext.SaveChangesAsync();
    }
    public async Task<Client?> RemoveClient(Guid guid){
        Client? client = cargoHubContext.Clients.Find(guid);
        if(client == null){
            return client;
        }
        cargoHubContext.Clients.Remove(client);
        await cargoHubContext.SaveChangesAsync();
        return client;
    }
    public void LoadFromJson(string path){
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            List<Client>? clients = JsonSerializer.Deserialize<List<Client>>(json);
            if(clients == null){
                return;
            }
            foreach(Client client in clients){
                SaveToDatabase(client);
            }
        }
    }
    public void SaveToDatabase(Client client) => AddClient(client);
}