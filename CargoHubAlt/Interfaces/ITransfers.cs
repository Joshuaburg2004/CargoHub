using Microsoft.AspNetCore.Http.Features;

public interface ITransfer
{
    public Task<List<Transfer>> GetTransfers();
    public Task<Transfer> GetTransferById(Guid transfer_id);
    public Task<List<Items>> GetItemsInTransfer(Guid transfer_id);
    public Task<bool> AddTransfer(Transfer transfer);
    public Task<bool> UpdateTransfer(Guid Transfer_id, Transfer transfer);
    public Task<bool> RemoveTransfer(Guid transfer_id);
}