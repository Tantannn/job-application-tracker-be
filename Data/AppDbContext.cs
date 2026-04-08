using JobTracker.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<User> Users => Set<User>();
  public DbSet<JobApplication> Applications => Set<JobApplication>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>(e =>
    {
      e.HasKey(u => u.Id);
      e.HasIndex(u => u.Email).IsUnique();
      e.Property(u => u.Email).HasMaxLength(256).IsRequired();
      e.Property(u => u.PasswordHash).IsRequired();
    });

    modelBuilder.Entity<JobApplication>(e =>
    {
      e.HasKey(u => u.Id);
      e.Property(a => a.Company).HasMaxLength(200).IsRequired();
      e.Property(a => a.Role).HasMaxLength(200).IsRequired();
      e.Property(a => a.Notes).HasMaxLength(200).IsRequired();

      e.Property(a => a.Status)
        .HasConversion<string>()
        .HasMaxLength(20);

      e.HasOne(a => a.User)
        .WithMany(u => u.Applications)
        .HasForeignKey(a => a.UserId)
        .OnDelete(DeleteBehavior.Cascade);
      
      e.HasIndex(a => a.UserId);
      e.HasIndex(a => new { a.UserId, a.Status });
    });
  }
  
  public override Task<int> SaveChangesAsync(CancellationToken ct = default)
  {
    foreach (var entry in ChangeTracker.Entries<JobApplication>())
    {
      if (entry.State == EntityState.Modified)
        entry.Entity.UpdatedAt = DateTime.UtcNow;
    }
    return base.SaveChangesAsync(ct);
  }
}