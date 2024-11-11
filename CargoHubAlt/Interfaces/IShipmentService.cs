using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface IShipmentService
    {
        public Task<Shipment?> GetShipment(int Id);
        public Task<IEnumerable<Shipment>> GetAllShipment();
        public Task<IEnumerable<ShipmentItem>?> GetItemsfromShipmentById(int Id);
        public Task<int?> AddShipment(Shipment shipment);
        public Task<Shipment?> UpdateShipment(int Id, Shipment shipment);
        public Task<Shipment?> DeleteShipment(int Id);
    }
}
