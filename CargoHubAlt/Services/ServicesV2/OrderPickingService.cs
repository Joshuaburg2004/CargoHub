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

        public async Task<PickingOrder> CreatePickingOrder(List<OrderedItem> order)
        {
            // Create a list of location id's
            // List<List<int>> locationIds = new List<List<int>>();
            List<int> locationIds = new List<int>();

            foreach (OrderedItem item in order)
            {
                // find inventory for each item (which contain location id's)
                Inventory? inventory = await _context.Inventories.FirstOrDefaultAsync(x => x.ItemId == item.ItemId);
                // add location id's of the current item to the list
                if (inventory != null)
                    locationIds.Add(inventory.Locations[0]);
            }

            locationIds.Sort();

            // create a list of locations
            List<Location> locations = new List<Location>();

            foreach (int locationId in locationIds)
            {
                Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == locationId);

                if (location != null)
                    locations.Add(location);
            }

            return new PickingOrder(1, locations);
        }
    }
}