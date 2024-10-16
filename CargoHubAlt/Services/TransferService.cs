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

    public async Task<Transfer> GetTransferById(Guid id) => await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Item>> GetItemsInTransfer(Guid id)
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

    public async Task<bool> RemoveTransfer(Guid id)
    {
        Transfer? transfer = await _context.Transfers.FirstOrDefaultAsync(x => x.Id == id);
        if (transfer == null) return false;
        _context.Transfers.Remove(transfer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTransfer(Guid id, Transfer transfer)
    {
        _context.Transfers.Update(transfer);
        await _context.SaveChangesAsync();
        return true;
    }
}