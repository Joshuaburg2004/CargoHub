using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;
using System.Text.Json;

namespace CargoHubAlt.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly CargoHubContext _context;

        public ShipmentService(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<Shipment>?> GetAllShipments()
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

        public async Task<List<ShipmentItem>?> GetItemsfromShipmentById(int id)
        {
            Shipment? shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
            if (shipment != null)
            {
                return shipment.Items;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<int>?> GetOrdersFromShipmentById(int id)
        {
            return await _context.Orders.Where(x => x.ShipmentId == id).Select(x => x.Id).ToListAsync();
        }

        public async Task<int?> AddShipment(Shipment shipment)
        {
            // Checks before adding a shipment
            if (shipment == null)
            {
                return null;
            }
            var existingShipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipment.Id);
            if (existingShipment != null)
            {
                return null;
            }

            // Add shipment
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment.Id;
        }

        public async Task<Shipment?> UpdateShipment(int shipmentid, Shipment shipment)
        {
            // Checks before updating a shipment
            Shipment? oldShipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipment.Id);
            if (oldShipment == null)
            {
                return null;
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

            return oldShipment;
        }

        public async Task<Shipment?> UpdateItemsInShipment(int id, List<ShipmentItem> items)
        {
            // Checks before updating items in a shipment
            var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
            if (shipment == null)
            {
                return null;
            }

            // Update items in shipment
            shipment.Items = items;

            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();

            return shipment;
        }

        public async Task UpdateOrdersInShipment(int id, List<int> orders)
        {
            var packedOrders = await GetOrdersFromShipmentById(id);
            if (packedOrders == null)
            {
                return;
            }
            foreach(var order in packedOrders)
            {
                if(!orders.Contains(order))
                {
                   var orderToUnpack = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order);
                   if(orderToUnpack != null)
                   {
                       orderToUnpack.ShipmentId = -1;
                       orderToUnpack.OrderStatus = "Scheduled";
                       _context.Orders.Update(orderToUnpack);
                   }
                }
            }
            foreach (var order in orders)
            {
                var orderToPack = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order);
                if (orderToPack != null)
                {
                    orderToPack.ShipmentId = id;
                    orderToPack.OrderStatus = "Packed";
                    _context.Orders.Update(orderToPack);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Shipment?> DeleteShipment(int id)
        {
            // Checks before deleting a shipment
            var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
            if (shipment == null)
            {
                return null;
            }

            // Delete shipment
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();

            return shipment;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Shipment>? shipments = JsonSerializer.Deserialize<List<Shipment>>(json);
                if (shipments == null)
                {
                    return;
                }
                foreach (Shipment shipment in shipments)
                {
                    await SaveToDatabase(shipment);
                }
            }
        }
        public async Task<int> SaveToDatabase(Shipment shipment){
            if(shipment is null){
                return -1;
            }
            if(shipment.OrderDate == null){shipment.OrderDate = "N/A";}
            if(shipment.RequestDate == null){shipment.RequestDate = "N/A";}
            if(shipment.ShipmentDate == null){shipment.ShipmentDate = "N/A";}
            if(shipment.ShipmentType == null){shipment.ShipmentType = "N/A";}
            if(shipment.ShipmentStatus == null){shipment.ShipmentStatus = "N/A";}
            if(shipment.Notes == null){shipment.Notes = "N/A";}
            if(shipment.CarrierCode == null){shipment.CarrierCode = "N/A";}
            if(shipment.CarrierDescription == null){shipment.CarrierDescription = "N/A";}
            if(shipment.ServiceCode == null){shipment.ServiceCode = "N/A";}
            if(shipment.PaymentType == null){shipment.PaymentType = "N/A";}
            if(shipment.TransferMode == null){shipment.TransferMode = "N/A";}
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment.Id;
        }
    }
}