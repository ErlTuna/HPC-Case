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
            await _context.Products.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
