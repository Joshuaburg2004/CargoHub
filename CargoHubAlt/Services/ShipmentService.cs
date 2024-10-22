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
    public async Task<Shipment> GetShipment(Guid id) => await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<bool> AddShipment(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateShipment(Shipment shipment)
    {
        Shipment oldshipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipment.Id);
        if (oldshipment == null) return false;

        oldshipment.Id = shipment.Id;
        oldshipment.Order_id = shipment.Order_id;
        oldshipment.Source_id = shipment.Source_id;
        oldshipment.Order_date = shipment.Order_date;
        oldshipment.Request_date = shipment.Request_date;
        oldshipment.Shipment_date = shipment.Shipment_date;
        oldshipment.Shipment_type = shipment.Shipment_type;
        oldshipment.Shipment_status = shipment.Shipment_status;
        oldshipment.Notes = shipment.Notes;
        oldshipment.Carrier_code = shipment.Carrier_code;
        oldshipment.Carrier_description = shipment.Carrier_description;
        oldshipment.Service_code = shipment.Service_code;
        oldshipment.Payment_type = shipment.Payment_type;
        oldshipment.Transfer_mode = shipment.Transfer_mode;
        oldshipment.Total_package_count = shipment.Total_package_count;
        oldshipment.Total_package_weight = shipment.Total_package_weight;
        oldshipment.Updated_at = shipment.Updated_at;
        oldshipment.Items = shipment.Items;

        _context.Shipments.Update(oldshipment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Update_items_in_Shipment(Guid id, Guid shipmentid, List<Item> items)
    {
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipmentid);
        if (shipment == null) return false;
        shipment.Items = items;
        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteShipment(Guid id)
    {
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
        if (shipment == null) return false;
        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
}