using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IOrderPickingServiceV2
    {
        public Task<PickingOrder> CreatePickingOrder(List<OrderedItem> order);
    }
}