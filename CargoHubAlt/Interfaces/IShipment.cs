public interface IShipmentService
{
    Task<List<Shipment>> GetShipments();
    Task<Shipment> GetShipment(int id);
    Task<bool> AddShipment(Shipment shipment);
    Task<bool> UpdateShipment(Shipment shipment);
    Task<bool> Update_items_in_Shipment(int id, int shipmentid, List<Item> items);
    Task<bool> DeleteShipment(int id);
}