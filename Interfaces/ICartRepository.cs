using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface ICartRepository
    {
        //Task<Cart?> GetByIdAsync(int id);
        Task<Cart?> GetByCustomerIdAsync(int customerId);
        Task<CartItem?> GetCartItemFromCartAsync(Cart cart, int productId);
        Task AddCartItemAsync(CartItem cartItem);
        void RemoveCartItem(CartItem cartItem);
        Task EmptyCartAsync(Cart cart);
        Task SaveAsync();
    }
}