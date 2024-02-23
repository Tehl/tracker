using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Models;

namespace Tracker.Domain.Context;

public class TrackerDbContext : DbContext
{
    public TrackerDbContext(
        DbContextOptions<TrackerDbContext> options
    )
    : base(options)
    {
    }

    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>()
            .ToTable("Activities")
            .HasKey(x => x.Id);
    }
}
