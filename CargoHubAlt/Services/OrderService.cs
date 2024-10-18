using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    readonly CargoHubContext _context;
    public OrderService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<bool> AddOrder(Order order)
    {
        await _context.Orders.AddAsync(order);
        if (await _context.SaveChangesAsync() >= 0)
            return true;
        return false;
    }

    public async Task<bool> RemoveOrder(Guid id)
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