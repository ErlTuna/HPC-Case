using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<bool> UpdatePriceAsync(int id, decimal newPrice);
        Task<bool> DeleteAsync(Product product);
        Task SaveAsync();
    }
}

