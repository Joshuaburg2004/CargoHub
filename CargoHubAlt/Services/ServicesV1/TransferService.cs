using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV1;
using System.Text.Json;
using CargoHubAlt.JsonModels;

namespace CargoHubAlt.Services.ServicesV1
{
    public class TransferServiceV1 : ITransferServiceV1
    {
        private readonly CargoHubContext _context;

        public TransferServiceV1(CargoHubContext context)
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

        public async Task<bool> CommitTransferById(int id)
        {
            Transfer? transfer = await GetTransferById(id);
            if (transfer == null) return false;
            var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var transferItem in transfer.Items)
                {
                    var inventories = _context.Inventories.Where(x => x.ItemId == transferItem.ItemId);
                    foreach (var inventory in inventories)
                    {
                        // assume the python code means to iterate over the locations, because as is the inventories dont end up being stored in one location but in several (with a list of locations per inventory)
                        // needs to be checked with PO
                        foreach (int ider in inventory.Locations)
                        {
                            if (ider == transfer.TransferFrom)
                            {
                                Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == transfer.TransferFrom);
                                if (location == null) return false;
                                location.localInventories.Where(x => x.InventoryId == ider).ToList().ForEach(x => x.Amount -= transferItem.Amount);
                                inventory.TotalOnHand -= transferItem.Amount;
                                inventory.TotalExpected = inventory.TotalOnHand + inventory.TotalOrdered;
                                inventory.TotalAvailable = inventory.TotalOnHand - inventory.TotalAllocated;
                                _context.Inventories.Update(inventory);
                            }
                            else if (ider == transfer.TransferTo)
                            {
                                Location? location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == transfer.TransferTo);
                                if (location == null) return false;
                                location.localInventories.Where(x => x.InventoryId == ider).ToList().ForEach(x => x.Amount += transferItem.Amount);
                                inventory.TotalOnHand += transferItem.Amount;
                                inventory.TotalExpected = inventory.TotalOnHand + inventory.TotalOrdered;
                                inventory.TotalAvailable = inventory.TotalOnHand - inventory.TotalAllocated;
                                _context.Inventories.Update(inventory);
                            }
                        }
                    }
                }
                transfer.TransferStatus = "Processed";
                Transfer? transfer1 = await UpdateTransfer(id, transfer);
                if (transfer1 == null) { return false; }
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task LoadFromJson(string path)
        {
            path = "data/" + path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<JsonTransfer>? transfers = JsonSerializer.Deserialize<List<JsonTransfer>>(json);
                if (transfers == null)
                {
                    return;
                }
                var transaction = _context.Database.BeginTransaction();
                foreach (JsonTransfer jsonTransfer in transfers)
                {
                    Transfer transfer = jsonTransfer.ToTransfer();
                    transfer.Items = jsonTransfer.items.Select(item => item.ToTransferItem()).ToList();
                    await SaveToDatabase(transfer);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<int> SaveToDatabase(Transfer transfer)
        {
            if (transfer is null)
            {
                return -1;
            }
            if (transfer.Reference == null) { transfer.Reference = "N/A"; }
            if (transfer.TransferStatus == null) { transfer.TransferStatus = "N/A"; }
            await _context.Transfers.AddAsync(transfer);
            return transfer.Id;
        }
    }
}