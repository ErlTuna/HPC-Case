using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Services
{
    public class ProductService : IProductService
    {
        readonly IProductRepository _repo;

        public ProductService(IProductRepository productRepository)
        {
            _repo = productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("Product cannot be null.", nameof(product));

            if (product.Name == string.Empty || product.Name == "")
                throw new InvalidOperationException("Product name is missing/empty.");

            if (product.Price < 0)
                throw new InvalidOperationException("Product price cannot be negative.");

            await _repo.AddAsync(product);
            await _repo.SaveAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _repo.GetAllAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("No product found that matches the given id.");

            return product;
        }

        public async Task UpdateProductAsync(int id, Product updatedProduct)
        {
            if (updatedProduct == null)
                throw new ArgumentNullException("The product to use in updating is null.", nameof(updatedProduct));

            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("No product found that matches the given id.");

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
                throw new KeyNotFoundException("No product found that matches the given id.");

            await _repo.DeleteAsync(product);
            await _repo.SaveAsync();
        }

    }
}


