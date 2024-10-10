using Microsoft.EntityFrameworkCore;

public class CargoHubContext : DbContext{
    public CargoHubContext(DbContextOptions<CargoHubContext> options) : base(options){}
    public DbSet<Client> Clients { get; set; }
}