using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface ICartService{
        
        Task<Cart> GetCartById(int id);
        Task<Cart> CreateCartForCustomerAsync(Customer Customer);
        Task<Cart> GetCartByCustomerId(int customerId);
        Task AddProductAsync(int customerId, int productId);
        Task RemoveProductAsync(int customerId, int productId);
        Task EmptyCartByCustomerIdAsync(int customerId);
    }
}