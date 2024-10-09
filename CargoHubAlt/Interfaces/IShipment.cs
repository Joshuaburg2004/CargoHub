public interface IShipmentService
{
    Task<List<Shipment>> GetShipments();
    Task<Shipment> GetShipment(Guid id);
    Task<bool> AddShipment(Shipment shipment);
    Task<bool> UpdateShipment(Shipment shipment);
    Task<bool> Update_items_in_Shipment(Guid id, Guid shipmentid, List<Item> items);
    Task<bool> DeleteShipment(Guid id);
}