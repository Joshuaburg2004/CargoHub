using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IOrderServiceV2
    {
        public Task<List<Order>?> GetOrders();
        public Task<List<Order>?> GetOrders(int? pageIndex);
        public Task<Order?> GetOrder(int orderId);
        public Task<IEnumerable<Order>?> GetPendingOrders();
        public Task<List<OrderedItem>?> GetOrderedItems(int orderId);
        public Task<bool> AddOrder(Order order);
        public Task<string> UpdateOrder(Order order);
        public Task<string> UpdateOrderedItems(int orderId, List<OrderedItem> items);
        public Task<bool> RemoveOrder(int orderId);
        public Task LoadFromJson(string path);
    }
}
