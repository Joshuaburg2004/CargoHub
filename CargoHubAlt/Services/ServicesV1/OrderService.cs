using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV1;
using System.Text.Json;

namespace CargoHubAlt.Services.ServicesV1
{
    public class OrderServiceV1 : IOrderServiceV1
    {
        readonly CargoHubContext _context;
        public OrderServiceV1(CargoHubContext context)
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

        public async Task<string> UpdateOrder(Order order)
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
                ChangedFields += "OrderDate, ";
            }
            if (oldOrder.OrderStatus != order.OrderStatus)
            {
                oldOrder.OrderStatus = order.OrderStatus;
                ChangedFields += "OrderStatus, ";
            }
            if (oldOrder.RequestDate != order.RequestDate)
            {
                oldOrder.RequestDate = order.RequestDate;
                ChangedFields += "RequestDate, ";
            }
            if (oldOrder.Reference != order.Reference)
            {
                oldOrder.Reference = order.Reference;
                ChangedFields += "Reference, ";
            }
            if (oldOrder.ReferenceExtra != order.ReferenceExtra)
            {
                oldOrder.ReferenceExtra = order.ReferenceExtra;
                ChangedFields += "ReferenceExtra, ";
            }
            if (oldOrder.Notes != order.Notes)
            {
                oldOrder.Notes = order.Notes;
                ChangedFields += "Notes, ";
            }
            if (oldOrder.ShippingNotes != order.ShippingNotes)
            {
                oldOrder.ShippingNotes = order.ShippingNotes;
                ChangedFields += "ShippingNotes, ";
            }
            if (oldOrder.PickingNotes != order.PickingNotes)
            {
                oldOrder.PickingNotes = order.PickingNotes;
                ChangedFields += "PickingNotes, ";
            }
            oldOrder.UpdatedAt = Base.GetTimeStamp();
            oldOrder.Items = order.Items;

            _context.Orders.Update(oldOrder);
            if (await _context.SaveChangesAsync() >= 0)
                return ChangedFields;
            return null;
        }

        public async Task<string> UpdateOrderedItems(int orderId, List<OrderedItem> items)
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