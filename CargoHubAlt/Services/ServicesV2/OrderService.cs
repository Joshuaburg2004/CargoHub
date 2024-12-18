using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV2
{
    public class OrderServiceV2 : IOrderServiceV2
    {
        readonly CargoHubContext _context;
        public OrderServiceV2(CargoHubContext context)
        {
            _context = context;
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

        public async Task<Order?> GetOrder(int orderId)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                return order;
            }
            return null;
        }

        public async Task<IEnumerable<Order>?> GetIncomingOrders()
        {
            List<Order> orders = await _context.Orders.Where(_ => _.OrderStatus != "Delivered").ToListAsync();
            
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
                return true;
            return false;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            // checks before updating Order
            Order? oldOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
            if (oldOrder == null)
            {
                return false;
            }

            // update Order
            oldOrder.SourceId = order.SourceId;
            oldOrder.Reference = order.Reference;
            oldOrder.ReferenceExtra = order.ReferenceExtra;
            oldOrder.OrderStatus = order.OrderStatus;
            oldOrder.Notes = order.Notes;
            oldOrder.ShippingNotes = order.ShippingNotes;
            oldOrder.PickingNotes = order.PickingNotes;
            oldOrder.WarehouseId = order.WarehouseId;
            oldOrder.ShipTo = order.ShipTo;
            oldOrder.BillTo = order.BillTo;
            oldOrder.ShipmentId = order.ShipmentId;
            oldOrder.TotalAmount = order.TotalAmount;
            oldOrder.TotalDiscount = order.TotalDiscount;
            oldOrder.TotalTax = order.TotalTax;
            oldOrder.TotalSurcharge = order.TotalSurcharge;
            oldOrder.CreatedAt = order.CreatedAt;
            oldOrder.UpdatedAt = Base.GetTimeStamp();
            oldOrder.Items = order.Items;

            _context.Orders.Update(oldOrder);
            if (await _context.SaveChangesAsync() >= 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateOrderedItems(int orderId, List<OrderedItem> items)
        {
            // checks before updating OrderedItems
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return false;
            }

            // update OrderedItems
            order.Items = items;

            _context.Orders.Update(order);
            if (await _context.SaveChangesAsync() >= 0)
                return true;
            return false;
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
                List<Order>? orders = JsonSerializer.Deserialize<List<Order>>(json);
                if (orders == null)
                {
                    return;
                }
                foreach (Order order in orders)
                {
                    await SaveToDatabase(order);
                }
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
            await _context.SaveChangesAsync();
            return order.Id;
        }


    }
}