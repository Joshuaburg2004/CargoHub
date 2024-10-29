using Microsoft.AspNetCore.Http.Features;

public interface ITransfer
{
    public Task<List<Transfer>> GetTransfers();
    public Task<Transfer> GetTransferById(int transfer_id);
    public Task<List<Item>> GetItemsInTransfer(int transfer_id);
    public Task<bool> AddTransfer(Transfer transfer);
    public Task<bool> UpdateTransfer(int Transfer_id, Transfer transfer);
    public Task<bool> RemoveTransfer(int transfer_id);
}