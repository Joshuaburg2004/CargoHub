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

        public async Task<Transfer?> GetTransferById(int id) => await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<TransferItem>?> GetItemsInTransfer(int id)
        {
            Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
            return transfer?.Items ?? null;
        }

        public async Task<int?> AddTransfer(Transfer transfer)
        {
            transfer.CreatedAt = Base.GetTimeStamp();
            transfer.UpdatedAt = Base.GetTimeStamp();
            await _context.Transfers.AddAsync(transfer);
            if (await this._context.SaveChangesAsync() >= 1) return transfer.Id;
            else return null;
        }

        public async Task<Transfer?> RemoveTransfer(int id)
        {
            Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
            if (transfer == null) return null;
            _context.Transfers.Remove(transfer);
            await _context.SaveChangesAsync();
            return transfer;
        }

        public async Task<Transfer?> UpdateTransfer(int id, Transfer transfer)
        {
            Transfer? oldTransfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
            if (oldTransfer == null) return oldTransfer;

            oldTransfer.Reference = transfer.Reference;
            oldTransfer.TransferFrom = transfer.TransferFrom;
            oldTransfer.TransferTo = transfer.TransferTo;
            oldTransfer.TransferStatus = transfer.TransferStatus;
            transfer.CreatedAt = transfer.CreatedAt;
            transfer.UpdatedAt = Base.GetTimeStamp();

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
                var transaction = _context.Database.BeginTransaction();
                foreach (Transfer transfer in transfers)
                {
                    await SaveToDatabase(transfer);
                }
                await transaction.CommitAsync();
            }
        }
        public async Task<int> SaveToDatabase(Transfer transfer){
            if(transfer is null){
                return -1;
            }
            if(transfer.Reference == null){transfer.Reference = "N/A";}
            if(transfer.TransferStatus == null){transfer.TransferStatus = "N/A";}
            if(transfer.TransferFrom == null){transfer.TransferFrom = 0;}
            if(transfer.TransferTo == null){transfer.TransferTo = 0;}
            await _context.Transfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
            return transfer.Id;
        }
    }
}