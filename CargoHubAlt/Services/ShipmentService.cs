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

    public async Task<bool> UpdateShipment(Shipment shipment)
    {
        // Checks before updating a shipment
        Shipment? oldShipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipment.Id);
        if (oldShipment == null)
        {
            return false;
        }

        // Update shipment
        oldShipment.OrderId = shipment.OrderId;
        oldShipment.SourceId = shipment.SourceId;
        oldShipment.OrderDate = shipment.OrderDate;
        oldShipment.RequestDate = shipment.RequestDate;
        oldShipment.ShipmentDate = shipment.ShipmentDate;
        oldShipment.ShipmentType = shipment.ShipmentType;
        oldShipment.ShipmentStatus = shipment.ShipmentStatus;
        oldShipment.Notes = shipment.Notes;
        oldShipment.CarrierCode = shipment.CarrierCode;
        oldShipment.CarrierDescription = shipment.CarrierDescription;
        oldShipment.ServiceCode = shipment.ServiceCode;
        oldShipment.PaymentType = shipment.PaymentType;
        oldShipment.TransferMode = shipment.TransferMode;
        oldShipment.TotalPackageCount = shipment.TotalPackageCount;
        oldShipment.TotalPackageWeight = shipment.TotalPackageWeight;
        oldShipment.CreatedAt = shipment.CreatedAt;
        oldShipment.UpdatedAt = DateTime.Now.ToString();
        oldShipment.Items = shipment.Items;

        _context.Shipments.Update(oldShipment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update_items_in_Shipment(int id, int shipmentid, List<ShipmentItem> items)
    {
        // Checks before updating items in a shipment
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipmentid);
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