using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HappenCodeECommerceAPIContext _context;

        public ProductRepository(HappenCodeECommerceAPIContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
            

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
            
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await SaveAsync();
        }

        public async Task<bool> UpdatePriceAsync(int id, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.Price = newPrice;
            await SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await SaveAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
