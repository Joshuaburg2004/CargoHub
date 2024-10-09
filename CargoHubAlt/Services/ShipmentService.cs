using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ShipmentService : IShipmentService
{
    public async Task<List<Shipment>> GetShipments()
    {
        throw new NotImplementedException();
    }
    public async Task<Shipment> GetShipment(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> AddShipment(Shipment shipment)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> UpdateShipment(Shipment shipment)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> Update_items_in_Shipment(int id, int shipmentid, List<Item> items)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteShipment(int id)
    {
        throw new NotImplementedException();
    }
}