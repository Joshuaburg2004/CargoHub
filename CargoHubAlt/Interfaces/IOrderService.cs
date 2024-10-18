public interface IOrderService
{
    public Task<bool> AddOrder(Order order);
}