using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface ITransferService
    {
        public Task<List<Transfer>> GetTransfers();
        public Task<Transfer?> GetTransferById(int transfer_id);
        public Task<List<TransferItem>?> GetItemsInTransfer(int transfer_id);
        public Task<int?> AddTransfer(Transfer transfer);
        public Task<Transfer?> UpdateTransfer(int Transfer_id, Transfer transfer);
        public Task<Transfer?> RemoveTransfer(int transfer_id);
        public Task CommitTransferById(int transfer_id);
        public Task LoadFromJson(string path);
    }
}
