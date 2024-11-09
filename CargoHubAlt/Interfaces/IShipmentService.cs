public interface IShipmentService
{
    Task<List<Shipment>?> GetShipments();
    Task<Shipment?> GetShipment(int id);
    Task<bool> AddShipment(Shipment shipment);
    Task<bool> UpdateShipment(int id, Shipment shipment);
    Task<bool> Update_items_in_Shipment(int id, List<ShipmentItem> items);
    Task<bool> Update_Order_in_Shipment(int id, Order order);
    Task<bool> DeleteShipment(int id);
}