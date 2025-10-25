using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HappenCodeECommerceAPI.Data;

public class HappenCodeECommerceAPIContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
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
