using Domain;
using Microsoft.EntityFrameworkCore;

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
            // Primary Key
            entity.HasKey(p => p.Id);

            // Properties
            entity.Property(p => p.Id)
                .HasColumnType("int")
                .IsRequired()
                .HasComment("The unique identifier for the product.");

            entity.Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("The name of the product.");

            entity.Property(p => p.IsAvailable)
                .HasColumnType("bit")
                .IsRequired()
                .HasComment("Indicates whether the product is available for purchase.");

            entity.Property(p => p.ManufactureEmail)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired()
                .HasComment("The email of the product manufacturer.");

            entity.Property(p => p.ManufacturePhone)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired()
                .HasComment("The phone number of the product manufacturer.");

            entity.Property(p => p.ProduceDate)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasComment("The date when the product was produced.");

            entity.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate }).IsUnique();
        });
    }
}