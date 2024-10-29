using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class TransferService : ITransfer
{
    private readonly CargoHubContext _context;

    public TransferService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<List<Transfer>> GetTransfers() => await _context.Transfers.ToListAsync();

    public async Task<Transfer> GetTransferById(int id) => await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Item>> GetItemsInTransfer(int id)
    {
        Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
        return transfer?.Items ?? new List<Item>();
    }

    public async Task<bool> AddTransfer(Transfer transfer)
    {
        await _context.Transfers.AddAsync(transfer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveTransfer(int id)
    {
        Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
        if (transfer == null) return false;
        _context.Transfers.Remove(transfer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTransfer(int id, Transfer transfer)
    {
        Transfer? oldTransfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
        if (oldTransfer == null) return false;

        oldTransfer.Reference = transfer.Reference;
        oldTransfer.Transfer_from = transfer.Transfer_from;
        oldTransfer.Transfer_to = transfer.Transfer_to;
        oldTransfer.Transfer_status = transfer.Transfer_status;
        oldTransfer.Updated_at = transfer.Updated_at;

        await _context.SaveChangesAsync();
        return true;
    }
}