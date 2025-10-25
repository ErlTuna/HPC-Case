using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Services
{
    public class ProductService : IProductService
    {
        readonly HappenCodeECommerceAPIContext _context;
        readonly IProductRepository _repo;

        public ProductService(HappenCodeECommerceAPIContext context, IProductRepository productRepository)
        {
            _context = context;
            _repo = productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            if (product.Name == string.Empty || product.Name == "")
                throw new ArgumentException("Product name is missing/empty.", nameof(product.Name));

            if (product.Price < 0)
                throw new ArgumentException("Product price cannot be negative, stupid chud.", nameof(product.Price));

            await _repo.AddAsync(product);
            await _repo.SaveAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _repo.GetAllAsync();

            if (products == null)
                throw new ArgumentNullException("No products found.");
            if (products.Any() == false)
                throw new ArgumentException("Products list exists but there aren't any items in it.");

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentNullException("No product found that matches the given id.");

            return product;
        }

        public async Task UpdateProductAsync(int id, Product updatedProduct)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentNullException("No product found that matches the given id.");

            if (updatedProduct.Name == string.Empty || updatedProduct.Name == "")
                throw new ArgumentException("Updated product doesn't have a name/has an empty name.");

            if (updatedProduct.Price < 0)
                throw new ArgumentException("Updated product price can not be negative.");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            await _repo.SaveAsync();
        }
        
        public async Task DeleteProductByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentNullException("No product found that matches the given id.");

            await _repo.DeleteAsync(product);
        }

        public async Task UpdatePriceAsync(int id, decimal newPrice)
        {

            if (newPrice < 0)
                throw new ArgumentException("Product price can not be negative, stupid chud.");

            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentNullException("Product could not be found.");
            
            product.Price = newPrice;
            await _repo.SaveAsync();
        }

    }
}


