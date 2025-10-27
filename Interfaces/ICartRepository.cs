using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> CreateCartForCustomerAsync(Customer customer);
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart?> GetByCustomerIdAsync(int customerId);
        Task AddCartItemAsync(CartItem cartItem);
        void RemoveCartItem(CartItem cartItem);
        Task<Cart?> GetCartWithItemsAsync(int customerId);
        void EmptyCartAsync(Cart cart);
        Task SaveAsync();
    }
}