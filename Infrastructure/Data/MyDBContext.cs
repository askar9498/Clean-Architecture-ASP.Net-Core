using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
            entity.Property(p => p.IsAvailable).IsRequired();
            entity.Property(p => p.ManufactureEmail).HasMaxLength(255).IsRequired();
            entity.Property(p => p.ManufacturePhone).HasMaxLength(20).IsRequired();
            entity.Property(p => p.ProduceDate).IsRequired();
            entity.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate }).IsUnique();
        });
    }
}