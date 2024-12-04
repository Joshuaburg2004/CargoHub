using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface IOrderServiceV1
    {
        public Task<List<Order>?> GetOrders();
        public Task<Order?> GetOrder(int orderId);
        public Task<List<OrderedItem>?> GetOrderedItems(int orderId);
        public Task<bool> AddOrder(Order order);
        public Task<bool> UpdateOrder(Order order);
        public Task<bool> UpdateOrderedItems(int orderId, List<OrderedItem> items);
        public Task<bool> RemoveOrder(int orderId);
        public Task LoadFromJson(string path);
    }
}
