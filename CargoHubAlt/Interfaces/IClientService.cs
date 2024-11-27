using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IClientService
    {
        public Task<IEnumerable<Client>> GetAllClients();
        public Task<Client?> GetClient(int id);
        public Task<int?> AddClient(Client client);
        public Task<Client?> RemoveClient(int id);
        public Task<Client?> UpdateClient(int id, Client client);
        public Task<List<Order>> GetOrdersByClient(int id);
        public Task LoadFromJson(string path);
    }
}
