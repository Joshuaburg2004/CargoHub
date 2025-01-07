using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV2
{
    public class ShipmentServiceV2 : IShipmentServiceV2
    {
        private readonly CargoHubContext _context;

        public ShipmentServiceV2(CargoHubContext context)
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

        public async Task<string?> UpdateShipment(int shipmentid, Shipment shipment)
        {
            // Checks before updating a shipment
            Shipment? oldShipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == shipment.Id);
            string ChangedFields = "";
            if (oldShipment == null)
            {
                return null;
            }

            // Update shipment
            if (oldShipment.OrderId != shipment.OrderId)
            {
                oldShipment.OrderId = shipment.OrderId;
                ChangedFields += $"OrderId, {shipment.OrderId}, ";
            }
            if (oldShipment.OrderDate != shipment.OrderDate)
            {
                oldShipment.OrderDate = shipment.OrderDate;
                ChangedFields += $"OrderDate, {shipment.OrderDate}, ";
            }
            if (oldShipment.RequestDate != shipment.RequestDate)
            {
                oldShipment.RequestDate = shipment.RequestDate;
                ChangedFields += $"RequestDate, {shipment.RequestDate}, ";
            }
            if (oldShipment.ShipmentDate != shipment.ShipmentDate)
            {
                oldShipment.ShipmentDate = shipment.ShipmentDate;
                ChangedFields += $"ShipmentDate, {shipment.ShipmentDate}, ";
            }
            if (oldShipment.ShipmentType != shipment.ShipmentType)
            {
                oldShipment.ShipmentType = shipment.ShipmentType;
                ChangedFields += $"ShipmentType, {shipment.ShipmentType}, ";
            }
            if (oldShipment.ShipmentStatus != shipment.ShipmentStatus)
            {
                oldShipment.ShipmentStatus = shipment.ShipmentStatus;
                ChangedFields += $"ShipmentStatus, {shipment.ShipmentStatus}, ";
            }
            if (oldShipment.Notes != shipment.Notes)
            {
                oldShipment.Notes = shipment.Notes;
                ChangedFields += $"Notes, {shipment.Notes}, ";
            }
            if (oldShipment.CarrierCode != shipment.CarrierCode)
            {
                oldShipment.CarrierCode = shipment.CarrierCode;
                ChangedFields += $"CarrierCode, {shipment.CarrierCode}, ";
            }
            if (oldShipment.CarrierDescription != shipment.CarrierDescription)
            {
                oldShipment.CarrierDescription = shipment.CarrierDescription;
                ChangedFields += $"CarrierDescription, {shipment.CarrierDescription}, ";
            }
            if (oldShipment.ServiceCode != shipment.ServiceCode)
            {
                oldShipment.ServiceCode = shipment.ServiceCode;
                ChangedFields += $"ServiceCode, {shipment.ServiceCode}, ";
            }
            if (oldShipment.PaymentType != shipment.PaymentType)
            {
                oldShipment.PaymentType = shipment.PaymentType;
                ChangedFields += $"PaymentType, {shipment.PaymentType}, ";
            }
            if (oldShipment.TransferMode != shipment.TransferMode)
            {
                oldShipment.TransferMode = shipment.TransferMode;
                ChangedFields += $"TransferMode, {shipment.TransferMode}, ";
            }
            if (oldShipment.TotalPackageCount != shipment.TotalPackageCount)
            {
                oldShipment.TotalPackageCount = shipment.TotalPackageCount;
                ChangedFields += $"TotalPackageCount, {shipment.TotalPackageCount}, ";
            }
            if (oldShipment.TotalPackageWeight != shipment.TotalPackageWeight)
            {
                oldShipment.TotalPackageWeight = shipment.TotalPackageWeight;
                ChangedFields += $"TotalPackageWeight, {shipment.TotalPackageWeight}, ";
            }

            oldShipment.UpdatedAt = DateTime.Now.ToString();
            oldShipment.Items = shipment.Items;

            _context.Shipments.Update(oldShipment);
            await _context.SaveChangesAsync();

            return ChangedFields;
        }

        public async Task<string?> UpdateItemsInShipment(int id, List<ShipmentItem> items)
        {
            // Checks before updating items in a shipment
            var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
            string ChangedFields = "";
            if (shipment == null)
            {
                return null;
            }
            var current = shipment.Items;
            if (current == null)
            {
                return null;
            }
            foreach (var item in current)
            {
                bool found = false;
                foreach (var newItem in items)
                {
                    if (item.ItemId == newItem.ItemId)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    var inventories = await _context.Inventories.Where(x => x.ItemId == item.ItemId).ToListAsync();
                    int maxOrdered = -1;
                    Inventory? maxInventory = null;
                    foreach (var inventory in inventories)
                    {
                        if (inventory.TotalOrdered > maxOrdered)
                        {
                            maxOrdered = inventory.TotalOrdered;
                            maxInventory = inventory;
                        }
                    }
                    if (maxInventory != null)
                    {
                        maxInventory.TotalOrdered -= item.Amount;
                        maxInventory.TotalExpected = maxInventory.TotalOnHand + maxInventory.TotalOrdered;
                        _context.Inventories.Update(maxInventory);
                    }
                }
            }
            foreach (var item2 in current)
            {
                foreach (var newItem2 in items)
                {
                    if (item2.ItemId == newItem2.ItemId)
                    {
                        var inventories = await _context.Inventories.Where(x => x.ItemId == item2.ItemId).ToListAsync();
                        int maxOrdered = -1;
                        Inventory? maxInventory = null;
                        foreach (var inventory in inventories)
                        {
                            if (inventory.TotalOrdered > maxOrdered)
                            {
                                maxOrdered = inventory.TotalOrdered;
                                maxInventory = inventory;
                            }
                        }
                        if (maxInventory != null)
                        {
                            maxInventory.TotalOrdered += newItem2.Amount - item2.Amount;
                            maxInventory.TotalExpected = maxInventory.TotalOnHand + maxInventory.TotalOrdered;
                            _context.Inventories.Update(maxInventory);
                        }
                    }
                }
            }

            // Update items in shipment
            shipment.Items = items;
            ChangedFields += $"Items, {items}";


            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();

            return ChangedFields;
        }

        public async Task UpdateOrdersInShipment(int id, List<int> orders)
        {
            var packedOrders = await GetOrdersFromShipmentById(id);
            if (packedOrders == null)
            {
                return;
            }
            foreach (var order in packedOrders)
            {
                if (!orders.Contains(order))
                {
                    var orderToUnpack = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order);
                    if (orderToUnpack != null)
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
        public async Task<int> SaveToDatabase(Shipment shipment)
        {
            if (shipment is null)
            {
                return -1;
            }
            if (shipment.OrderDate == null) { shipment.OrderDate = "N/A"; }
            if (shipment.RequestDate == null) { shipment.RequestDate = "N/A"; }
            if (shipment.ShipmentDate == null) { shipment.ShipmentDate = "N/A"; }
            if (shipment.ShipmentType == null) { shipment.ShipmentType = "N/A"; }
            if (shipment.ShipmentStatus == null) { shipment.ShipmentStatus = "N/A"; }
            if (shipment.Notes == null) { shipment.Notes = "N/A"; }
            if (shipment.CarrierCode == null) { shipment.CarrierCode = "N/A"; }
            if (shipment.CarrierDescription == null) { shipment.CarrierDescription = "N/A"; }
            if (shipment.ServiceCode == null) { shipment.ServiceCode = "N/A"; }
            if (shipment.PaymentType == null) { shipment.PaymentType = "N/A"; }
            if (shipment.TransferMode == null) { shipment.TransferMode = "N/A"; }
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment.Id;
        }
        public async Task<bool> CommitShipmentById(int id)
        {
            if (id <= 0)
                return false;
            var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
            if (shipment == null)
                return false;
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == shipment.OrderId);
            if (order == null)
                return false;
            if (shipment.ShipmentStatus == "Pending")
            {
                shipment.ShipmentStatus = "Transit";
                /* 
                The following code needs to be confirmed, will be checked with PO if this is intention.
                Question is: Should order have itself updated and should the locations be updated based on the order?
                Source Id should be inventory too.
                */
                var transaction = _context.Database.BeginTransaction();
                foreach (var shipmentItem in order.Items)
                {
                    var inventories = _context.Inventories.Where(x => x.ItemId == shipmentItem.ItemId);
                    foreach (var inventory in inventories)
                    {
                        // assume the python code means to iterate over the locations, because as is the inventories dont end up being stored in one location but in several (with a list of locations per inventory)
                        // needs to be checked with PO
                        foreach (int ider in inventory.Locations)
                        {
                            if (ider == order.SourceId)
                            {
                                inventory.TotalOnHand -= shipmentItem.Amount;
                                inventory.TotalExpected = inventory.TotalOnHand + inventory.TotalOrdered;
                                inventory.TotalAvailable = inventory.TotalOnHand - inventory.TotalAllocated;
                                _context.Inventories.Update(inventory);
                            }
                        }
                    }
                }
                order.OrderStatus = "Transit";
                _context.Orders.Update(order);
                _context.Shipments.Update(shipment);
                await _context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            else if (shipment.ShipmentStatus == "Transit")
            {
                shipment.ShipmentStatus = "Delivered";
                var transaction = _context.Database.BeginTransaction();
                foreach (var shipmentItem in order.Items)
                {
                    var inventories = _context.Inventories.Where(x => x.ItemId == shipmentItem.ItemId);
                    foreach (var inventory in inventories)
                    {
                        // assume the python code means to iterate over the locations, because as is the inventories dont end up being stored in one location but in several (with a list of locations per inventory)
                        // needs to be checked with PO
                        foreach (int ider in inventory.Locations)
                        {
                            if (ider == order.ShipTo)
                            {
                                inventory.TotalOnHand -= shipmentItem.Amount;
                                inventory.TotalExpected = inventory.TotalOnHand + inventory.TotalOrdered;
                                inventory.TotalAvailable = inventory.TotalOnHand - inventory.TotalAllocated;
                                _context.Inventories.Update(inventory);
                            }
                        }
                    }
                }

                order.OrderStatus = "Delivered";
                _context.Shipments.Update(shipment);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}