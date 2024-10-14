using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ItemsService : IItemsService
{
    readonly DbContext _context;
    public ItemsService(DbContext context)
    {
        _context = context;
    }

    public async Task<Item[]> GetItems(string[] ids)
    {
        throw new NotImplementedException();
    }

    public async Task GetItem(string id)
    {
        throw new NotImplementedException();
    }

    public async Task GetItemsForItemLine()
    {
        throw new NotImplementedException();
    }

    public async Task GetItemsForItemGroup()
    {
        throw new NotImplementedException();
    }

    public async Task GetItemsForItemType()
    {
        throw new NotImplementedException();
    }

    public async Task GetItemsForSupplier()
    {
        throw new NotImplementedException();
    }

    public async Task AddItem()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateItem()
    {
        throw new NotImplementedException();
    }

    public async Task RemoveItem()
    {
        throw new NotImplementedException();
    }

    public async Task Load()
    {
        throw new NotImplementedException();
    }

    public async Task Save()
    {
        throw new NotImplementedException();
    }
}