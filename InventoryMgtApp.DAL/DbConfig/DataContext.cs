using InventoryMgtApp.DAL.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgtApp.DAL.DbConfig;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<ProductSale> ProductSales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductSale>()
            .HasKey(x => new { x.ProductId, x.SaleId });

        modelBuilder.Entity<ProductSale>()
            .HasOne(x => x.Sale)
            .WithMany(x => x.ProductSales)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
