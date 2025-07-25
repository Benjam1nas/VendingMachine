using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<MachineItem> MachineItems { get; set; }
    public DbSet<MachineCoin> MachineCoins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
