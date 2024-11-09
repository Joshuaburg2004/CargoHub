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

    public async Task<List<Shipment>?> GetShipments()
    {
        List<Shipment> shipments = await _context.Shipments.ToListAsync();
        if (shipments != null)
        {
            return shipments;
        }
        else
        {
            return null;
        }
    }

    public async Task<Shipment?> GetShipment(int id)
    {
        Shipment? shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
        if (shipment != null)
        {
            return shipment;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> AddShipment(Shipment shipment)
    {
        // Checks before adding a shipment
        if (shipment == null)
        {
            return false;
        }
        var existingShipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipment.Id);
        if (existingShipment != null)
        {
            return false;
        }

        // Add shipment
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateShipment(int id, Shipment shipment)
    {
        // Checks before updating a shipment
        Shipment? oldShipment = await _context.Shipments.FindAsync(id);
        if (oldShipment == null)
        {
            return false;
        }

        // Update shipment
        oldShipment.Order_Id = shipment.Order_Id;
        oldShipment.Source_Id = shipment.Source_Id;
        oldShipment.Order_Date = shipment.Order_Date;
        oldShipment.Request_Date = shipment.Request_Date;
        oldShipment.Shipment_Date = shipment.Shipment_Date;
        oldShipment.Shipment_Type = shipment.Shipment_Type;
        oldShipment.Shipment_Status = shipment.Shipment_Status;
        oldShipment.Notes = shipment.Notes;
        oldShipment.Carrier_Code = shipment.Carrier_Code;
        oldShipment.Carrier_Description = shipment.Carrier_Description;
        oldShipment.Service_Code = shipment.Service_Code;
        oldShipment.Payment_Type = shipment.Payment_Type;
        oldShipment.Transfer_Mode = shipment.Transfer_Mode;
        oldShipment.Total_Package_Count = shipment.Total_Package_Count;
        oldShipment.Total_Package_Weight = shipment.Total_Package_Weight;
        oldShipment.Updated_At = DateTime.Now.ToString();
        oldShipment.Items = shipment.Items;

        _context.Shipments.Update(oldShipment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update_items_in_Shipment(int id, List<ShipmentItem> items)
    {
        // Checks before updating items in a shipment
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
        if (shipment == null)
        {
            return false;
        }

        // Update items in shipment
        shipment.Items = items;

        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update_Order_in_Shipment(int id, Order order)
    {
        // Checks before updating order in a shipment
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return false;
        }

        // Update order in shipment
        shipment.Order_Id = order.Id;

        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteShipment(int id)
    {
        // Checks before deleting a shipment
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
        if (shipment == null)
        {
            return false;
        }

        // Delete shipment
        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();

        return true;
    }
}