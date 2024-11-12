using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces;
using System.Text.Json;

namespace CargoHubAlt.Services
{
    public class TransferService : ITransferService
    {
        private readonly CargoHubContext _context;

        public TransferService(CargoHubContext context)
        {
            _context = context;
        }

        public async Task<List<Transfer>> GetTransfers() => await _context.Transfers.ToListAsync();

        public async Task<Transfer?> GetTransferById(int id) => await _context.Transfers.FirstOrDefaultAsync(x => x.id == id);

        public async Task<List<TransferItem>?> GetItemsInTransfer(int id)
        {
            Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.id == id);
            return transfer?.items ?? null;
        }

        public async Task<int?> AddTransfer(Transfer transfer)
        {
            transfer.created_at = Base.GetTimeStamp();
            transfer.updated_at = Base.GetTimeStamp();
            await _context.Transfers.AddAsync(transfer);
            if (await this._context.SaveChangesAsync() >= 1) return transfer.id;
            else return null;
        }

        public async Task<Transfer?> RemoveTransfer(int id)
        {
            Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.id == id);
            if (transfer == null) return null;
            _context.Transfers.Remove(transfer);
            await _context.SaveChangesAsync();
            return transfer;
        }

        public async Task<Transfer?> UpdateTransfer(int id, Transfer transfer)
        {
            Transfer? oldTransfer = await _context.Transfers.FirstOrDefaultAsync(x => x.id == id);
            if (oldTransfer == null) return oldTransfer;

            oldTransfer.reference = transfer.reference;
            oldTransfer.transfer_from = transfer.transfer_from;
            oldTransfer.transfer_to = transfer.transfer_to;
            oldTransfer.transfer_status = transfer.transfer_status;
            transfer.created_at = transfer.created_at;
            transfer.updated_at = Base.GetTimeStamp();

            await _context.SaveChangesAsync();
            return oldTransfer;
        }
        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Transfer>? transfers = JsonSerializer.Deserialize<List<Transfer>>(json);
                if (transfers == null)
                {
                    return;
                }
                foreach (Transfer transfer in transfers)
                {
                    await SaveToDatabase(transfer);
                }
            }
        }
        public async Task<int> SaveToDatabase(Transfer transfer){
            if(transfer is null){
                return -1;
            }
            if(transfer.reference == null){transfer.reference = "N/A";}
            if(transfer.transfer_status == null){transfer.transfer_status = "N/A";}
            if(transfer.transfer_from == null){transfer.transfer_from = 0;}
            if(transfer.transfer_to == null){transfer.transfer_to = 0;}
            await _context.Transfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
            return transfer.id;
        }
    }
}