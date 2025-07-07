using PayTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PayTrack.Infrastructure.Context;

public class PayTrackDbContext(DbContextOptions<PayTrackDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}
