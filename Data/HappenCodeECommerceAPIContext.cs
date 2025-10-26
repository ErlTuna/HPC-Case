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
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany()  // Product does not track CartItems
            .HasForeignKey(ci => ci.ProductId);
    }
    public HappenCodeECommerceAPIContext(DbContextOptions<HappenCodeECommerceAPIContext> options)
            : base(options)
    {
    }

    
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=mydb;Username=myuser;Password=mypassword");
    }
    */


}
