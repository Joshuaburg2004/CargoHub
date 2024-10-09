interface IClients{
    public IEnumerable<Client> GetClients();
    public Client? GetClient(int id);
    public void AddClient(Client client);
    public void RemoveClient(Client client);
    public void UpdateClient(Client client);
}