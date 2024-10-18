public interface IOrderService
{
    public Task<bool> AddOrder(Order order);
    public Task<bool> RemoveOrder(Guid id);
}