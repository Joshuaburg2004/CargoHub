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
}