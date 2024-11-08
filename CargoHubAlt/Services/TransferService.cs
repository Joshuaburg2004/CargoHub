using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class TransferService : ITransferService
{
    private readonly CargoHubContext _context;

    public TransferService(CargoHubContext context)
    {
        _context = context;
    }

    public async Task<List<Transfer>> GetTransfers() => await _context.Transfers.ToListAsync();

    public async Task<Transfer> GetTransferById(int id) => await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<TransferItem>?> GetItemsInTransfer(int id)
    {
        Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
        return transfer?.Items ?? null;
    }

    public async Task<int?> AddTransfer(Transfer transfer)
    {
        transfer.Created_At = Base.GetTimeStamp();
        transfer.Updated_At = Base.GetTimeStamp();
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
        oldTransfer.Transfer_From = transfer.Transfer_From;
        oldTransfer.Transfer_To = transfer.Transfer_To;
        oldTransfer.Transfer_Status = transfer.Transfer_Status;
        transfer.Created_At = transfer.Created_At;
        transfer.Updated_At = Base.GetTimeStamp();

        await _context.SaveChangesAsync();
        return oldTransfer;
    }
}