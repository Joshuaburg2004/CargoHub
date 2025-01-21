using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IOrderPickingServiceV2
    {
        public Task<List<PickingOrder>> GetPickingOrdersForWarehouse(int warehouseId);
        public Task<List<PickingOrder>> GetPickingOrdersForOrder(int orderId);
        public Task<bool> CreatePickingOrders(List<OrderedItem> order, int orderId);
        public Task<bool> DeletePickingOrder(int pickingOrderId);
        public Task<bool> DeletePickingOrdersForOrder(int orderId);
        public Task<bool> CompletePickingOrder(int pickingOrderId);
        public Task<bool> ClearTable();
    }
}