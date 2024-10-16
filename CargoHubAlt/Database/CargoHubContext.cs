using Microsoft.EntityFrameworkCore;

public class CargoHubContext : DbContext{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Item_group> Item_Groups {get; set;}
}