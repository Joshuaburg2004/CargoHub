public class Clients{
    private readonly CargoHubContext cargoHubContext;
    public Clients(CargoHubContext context){
        cargoHubContext = context;
    }
    public IEnumerable<Client> GetClients(){
        return cargoHubContext.Clients;
    }
    public Client? GetClient(int id){
        return cargoHubContext.Clients.Find(id);
    }
    public void AddClient(Client client){
        cargoHubContext.Clients.Add(client);
        cargoHubContext.SaveChanges();
    }
}