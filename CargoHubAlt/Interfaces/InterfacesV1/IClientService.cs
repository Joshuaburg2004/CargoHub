using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface IClientServiceV1
    {
        public Task<List<Client>> GetAllClients();
        public Task<Client?> GetClient(int id);
        public Task<int?> AddClient(Client client);
        public Task<Client?> RemoveClient(int id);
        public Task<string?> UpdateClient(int id, Client client);
        public Task<List<Order>> GetOrdersByClient(int id);
        public Task LoadFromJson(string path);
    }
}
