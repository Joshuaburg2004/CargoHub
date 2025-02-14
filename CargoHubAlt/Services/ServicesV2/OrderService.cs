using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV2
{
    public class OrderServiceV2 : IOrderServiceV2
    {
        readonly CargoHubContext _context;
        readonly IOrderPickingServiceV2 _orderPickingService;
        public OrderServiceV2(CargoHubContext context, IOrderPickingServiceV2 orderPickingService)
        {
            _context = context;
            _orderPickingService = orderPickingService;
        }

        public async Task<List<Order>?> GetOrders()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            if (orders != null)
            {
                return orders;
            }
            return null;
        }

        public async Task<List<Order>?> GetOrders(int? pageIndex)
        {
            if (pageIndex == null)
            {
                return await GetOrders();
            }
            int page = (int)pageIndex;
            var orders = await _context.Orders
                .OrderBy(c => c.Id)
                .Skip((page - 1) * 30)
                .Take(30)
                .ToListAsync();
            return orders;
        }

        public async Task<Order?> GetOrder(int orderId)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                return order;
            }
            return null;
        }

        public async Task<IEnumerable<Order>?> GetPendingOrders()
        {
            List<Order> orders = await _context.Orders.Where(_ => _.OrderStatus == "Pending").ToListAsync();

            return orders;
        }
        public async Task<List<OrderedItem>?> GetOrderedItems(int orderId)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null && order.Items != null)
            {
                return order.Items;
            }
            return null;
        }

        public async Task<bool> AddOrder(Order order)
        {
            // checks before adding Order
            if (order == null)
            {
                return false;
            }
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
            if (existingOrder != null)
            {
                return false;
            }

            // add Order
            await _context.Orders.AddAsync(order);

            if (await _context.SaveChangesAsync() >= 0)
            {
                // Create picking orders
                int orderId = order.Id;
                await _orderPickingService.CreatePickingOrders(order.Items, orderId);

                return true;
            }
            return false;
        }

        public async Task<string?> UpdateOrder(Order order)
        {
            // checks before updating Order
            Order? oldOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
            string ChangedFields = "";
            if (oldOrder == null)
            {
                return null;
            }

            // update Order
            if (oldOrder.OrderDate != order.OrderDate)
            {
                oldOrder.OrderDate = order.OrderDate;
                ChangedFields += $"OrderDate: {order.OrderDate}, ";
            }
            if (oldOrder.OrderStatus != order.OrderStatus)
            {
                oldOrder.OrderStatus = order.OrderStatus;
                ChangedFields += $"OrderStatus: {order.OrderStatus}, ";
            }
            if (oldOrder.RequestDate != order.RequestDate)
            {
                oldOrder.RequestDate = order.RequestDate;
                ChangedFields += $"RequestDate: {order.RequestDate}, ";
            }
            if (oldOrder.Reference != order.Reference)
            {
                oldOrder.Reference = order.Reference;
                ChangedFields += $"Reference: {order.Reference}, ";
            }
            if (oldOrder.ReferenceExtra != order.ReferenceExtra)
            {
                oldOrder.ReferenceExtra = order.ReferenceExtra;
                ChangedFields += $"ReferenceExtra: {order.ReferenceExtra}, ";
            }
            if (oldOrder.Notes != order.Notes)
            {
                oldOrder.Notes = order.Notes;
                ChangedFields += $"Notes: {order.Notes}, ";
            }
            if (oldOrder.ShippingNotes != order.ShippingNotes)
            {
                oldOrder.ShippingNotes = order.ShippingNotes;
                ChangedFields += $"ShippingNotes: {order.ShippingNotes}, ";
            }
            if (oldOrder.PickingNotes != order.PickingNotes)
            {
                oldOrder.PickingNotes = order.PickingNotes;
                ChangedFields += $"PickingNotes: {order.PickingNotes}, ";
            }
            oldOrder.UpdatedAt = Base.GetTimeStamp();
            oldOrder.Items = order.Items;

            // update Order
            _context.Orders.Update(oldOrder);

            // update PickingOrders
            await _orderPickingService.DeletePickingOrder(oldOrder.Id);
            await _orderPickingService.CreatePickingOrders(order.Items, order.Id);

            if (await _context.SaveChangesAsync() >= 0)
                return ChangedFields;
            return null;
        }

        public async Task<string?> UpdateOrderedItems(int orderId, List<OrderedItem> items)
        {
            // checks before updating OrderedItems
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            string ChangedFields = "";
            if (order == null)
            {
                return null;
            }

            // update OrderedItems
            if (order.Items != items)
            {
                order.Items = items;
                ChangedFields += "Items, ";
            }

            _context.Orders.Update(order);
            if (await _context.SaveChangesAsync() >= 0)
                return ChangedFields;
            return null;
        }

        public async Task<bool> RemoveOrder(int id)
        {
            // checks before removing Order
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            // remove Order
            _context.Orders.Remove(order);

            // remove PickingOrders
            await _orderPickingService.DeletePickingOrdersForOrder(id);

            if (await _context.SaveChangesAsync() >= 0)
                return true;
            return false;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonOrder>? orders = JsonSerializer.Deserialize<List<JsonOrder>>(json);
                if (orders == null)
                {
                    return;
                }
                var transaction = _context.Database.BeginTransaction();
                foreach (JsonOrder jsonOrder in orders)
                {
                    Order order = jsonOrder.ToOrder();
                    await SaveToDatabase(order);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<int> SaveToDatabase(Order order)
        {
            if (order is null)
            {
                return -1;
            }
            if (order.OrderDate == null) { order.OrderDate = "N/A"; }
            if (order.OrderStatus == null) { order.OrderStatus = "N/A"; }
            if (order.RequestDate == null) { order.RequestDate = "N/A"; }
            if (order.Reference == null) { order.Reference = "N/A"; }
            if (order.ReferenceExtra == null) { order.ReferenceExtra = "N/A"; }
            if (order.Notes == null) { order.Notes = "N/A"; }
            if (order.ShippingNotes == null) { order.ShippingNotes = "N/A"; }
            if (order.PickingNotes == null) { order.PickingNotes = "N/A"; }

            await _context.Orders.AddAsync(order);
            return order.Id;
        }
    }
}