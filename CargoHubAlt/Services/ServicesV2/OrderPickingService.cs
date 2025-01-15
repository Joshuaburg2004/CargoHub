using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace CargoHubAlt.Services.ServicesV2
{
    public class OrderPickingServiceV2 : IOrderPickingServiceV2
    {
        readonly CargoHubContext _context;
        public OrderPickingServiceV2(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<PickingOrder>> GetPickingOrdersForWarehouse(int warehouseId)
        {
            return await _context.PickingOrders.Where(x => x.WarehouseId == warehouseId).ToListAsync();
        }

        public async Task<List<PickingOrder>> GetPickingOrdersForOrder(int orderId)
        {
            return await _context.PickingOrders.Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<bool> CreatePickingOrders(List<OrderedItem> order, int orderId)
        {
            // Create a dictionary of location id's per item
            Dictionary<string, List<LocationWithStock>> locations = new Dictionary<string, List<LocationWithStock>>();
            foreach (OrderedItem item in order)
            {
                // find inventory for each item (which contain location id's)
                Inventory? inventory = await _context.Inventories.FirstOrDefaultAsync(x => x.ItemId == item.ItemId);
                // add location id's of the current item to the dictionary
                if (item.ItemId != null && inventory != null)
                {
                    // create a list of locations and their stock for the current item
                    List<LocationWithStock> locationList = new List<LocationWithStock>();
                    foreach (int locationId in inventory.Locations)
                    {
                        Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == locationId);
                        int stock = new Random().Next(0, 200);
                        if (location != null)
                        {
                            LocationWithStock locationWithStock = new LocationWithStock(location, stock);
                            locationList.Add(locationWithStock);
                        }
                    }
                    locations.Add(item.ItemId, locationList);
                }
            }

            // CLuster locations by warehouse
            Dictionary<int, HashSet<string>> warehouseToItems = new Dictionary<int, HashSet<string>>();
            foreach (var kvp in locations)
            {
                string itemId = kvp.Key;
                foreach (LocationWithStock locationWIthStock in kvp.Value)
                {
                    int warehouseId = locationWIthStock.Location.WarehouseId;
                    if (!warehouseToItems.ContainsKey(warehouseId))
                    {
                        warehouseToItems[warehouseId] = new HashSet<string>();
                    }
                    warehouseToItems[warehouseId].Add(itemId);
                }
            }

            // Choose the best warehouses to cover all items
            HashSet<string> itemsToPick = new HashSet<string>(locations.Keys);
            List<int> selectedWarehouses = new List<int>();

            while (itemsToPick.Count > 0)
            {
                // Find the warehouse that covers the most remaining items
                int bestWarehouse = -1;
                int maxCoverage = 0;

                foreach (var kvp in warehouseToItems)
                {
                    int warehouseId = kvp.Key;
                    var itemsInWarehouse = kvp.Value;
                    int coverage = itemsInWarehouse.Count(item => itemsToPick.Contains(item));

                    if (coverage > maxCoverage)
                    {
                        bestWarehouse = warehouseId;
                        maxCoverage = coverage;
                    }
                }

                // Add the best warehouse to the selection
                selectedWarehouses.Add(bestWarehouse);

                // Remove the items covered by the selected warehouse so no items are picked twice
                foreach (var item in warehouseToItems[bestWarehouse])
                {
                    itemsToPick.Remove(item);
                }
            }

            // Create a picking order for each selected warehouse
            foreach (int warehouseId in selectedWarehouses)
            {
                List<Location> route = new List<Location>();
                foreach (var kvp in locations)
                {
                    string itemId = kvp.Key;
                    foreach (LocationWithStock locationWithStock in kvp.Value)
                    {
                        if (locationWithStock.Location.WarehouseId == warehouseId)
                        {
                            route.Add(locationWithStock.Location);
                        }
                    }
                }

                PickingOrder pickingOrder = new PickingOrder(orderId, warehouseId, route);
                await _context.PickingOrders.AddAsync(pickingOrder);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CompletePickingOrder(int pickingOrderId)
        {
            PickingOrder? pickingOrder = await _context.PickingOrders.FirstOrDefaultAsync(x => x.Id == pickingOrderId);
            if (pickingOrder != null)
            {
                pickingOrder.IsCompleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

public class LocationWithStock
{
    public Location Location { get; set; }
    public int Stock { get; set; }

    public LocationWithStock(Location location, int stock)
    {
        Location = location;
        Stock = stock;
    }
}