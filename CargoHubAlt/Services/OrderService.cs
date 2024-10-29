using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    readonly CargoHubContext _context;
    public OrderService(CargoHubContext context)
    {
        _context = context;
    }

    public Task<List<Order>> GetOrders()
    {

    }

    public Task<Order> GetOrder(int orderId)
    {

    }

    public Task<List<OrderedItem>> GetOrderedItems(int orderId)
    {

    }

    public async Task<bool> AddOrder(Order order)
    {
        await _context.Orders.AddAsync(order);
        if (await _context.SaveChangesAsync() >= 0)
            return true;
        return false;
    }

    public Task<bool> UpdateOrder(Order order)
    {

    }

    public Task<bool> UpdateOrderedItems(int orderId, List<OrderedItem> items)
    {

    }

    public async Task<bool> RemoveOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return false;
        _context.Orders.Remove(order);
        if (await _context.SaveChangesAsync() >= 0)
            return true;
        return false;
    }
}