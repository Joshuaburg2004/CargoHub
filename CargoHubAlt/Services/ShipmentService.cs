using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ShipmentService : IShipmentService
{
    private readonly CargoHubContext _context;

    public ShipmentService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<List<Shipment>> GetShipments() => await _context.Shipments.ToListAsync();
    public async Task<Shipment> GetShipment(Guid id) => await _context.Shipments.FirstOrDefaultAsync(x => x.id == id);
    public async Task<bool> AddShipment(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateShipment(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Update_items_in_Shipment(Guid id, Guid shipmentid, List<Item> items)
    {
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.id == shipmentid);
        if (shipment == null) return false;
        shipment.items = items;
        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteShipment(Guid id)
    {
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.id == id);
        if (shipment == null) return false;
        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
}