using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(int id, Product updatedProduct);
        Task DeleteProductByIdAsync(int id);
        Task UpdatePriceAsync(int id, decimal newPrice);
    }
}


