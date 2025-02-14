using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IShipmentServiceV2
    {
        public Task<Shipment?> GetShipment(int Id);
        public Task<List<Shipment>?> GetAllShipments();
        public Task<List<Shipment>?> GetAllShipments(int? pageIndex);
        public Task<List<ShipmentItem>?> GetItemsfromShipmentById(int Id);
        public Task<List<int>?> GetOrdersFromShipmentById(int id);
        public Task<int?> AddShipment(Shipment shipment);
        public Task<string?> UpdateShipment(int shipmentid, Shipment shipment);
        public Task<Shipment?> DeleteShipment(int Id);
        public Task<string?> UpdateItemsInShipment(int id, List<ShipmentItem> items);
        public Task UpdateOrdersInShipment(int id, List<int> orders);
        public Task LoadFromJson(string path);
        public Task<bool> CommitShipmentById(int id);
    }
}
