using Microsoft.EntityFrameworkCore;
using Market.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail>? ProductDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product konfiguratsiyasi
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.ModifiedAt).HasDefaultValueSql("now()");
        });

        // ProductDetail konfiguratsiyasi
        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasOne(e => e.Product)
                .WithOne(p => p.ProductDetail)
                .HasForeignKey<ProductDetail>(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}