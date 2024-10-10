public interface IClients{
    public Task<IEnumerable<Client>> GetAllClients();
    public Task<IEnumerable<Client>> GetBatchClients(Guid[] guids);
    public Task<Client?> GetClient(Guid guid);
    public Task<Guid?> AddClient(Client client);
    public Task<Client?> RemoveClient(Guid guid);
    public Task<Client?> UpdateClient(Guid guid, Client client);
}