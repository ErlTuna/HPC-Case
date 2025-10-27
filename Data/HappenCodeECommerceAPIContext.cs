using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HappenCodeECommerceAPI.Data;

public class HappenCodeECommerceAPIContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cart -> CartItems (one-to-many?)
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId);

        // CartItem -> Product (one-to-one)
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId);

        // Customer -> Cart (one-to-one)
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Cart)
            .WithOne(ca => ca.Customer)
            .HasForeignKey<Cart>(ca => ca.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    public HappenCodeECommerceAPIContext(DbContextOptions<HappenCodeECommerceAPIContext> options)
            : base(options)
    {
    }

}
