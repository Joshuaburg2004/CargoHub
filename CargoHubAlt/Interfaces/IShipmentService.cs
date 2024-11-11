using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IShipmentService
    {
        public Task<Shipment?> GetShipment(int Id);
        public Task<List<Shipment>?> GetAllShipments();
        public Task<List<ShipmentItem>?> GetItemsfromShipmentById(int Id);
        public Task<int?> AddShipment(Shipment shipment);
        public Task<Shipment?> UpdateShipment(int shipmentid, Shipment shipment);
        public Task<Shipment?> DeleteShipment(int Id);
        public Task<Shipment?> Update_items_in_Shipment(int id, int shipmentid, List<ShipmentItem> items);
    }
}
