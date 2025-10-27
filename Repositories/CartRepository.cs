using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly HappenCodeECommerceAPIContext _context;

        public CartRepository(HappenCodeECommerceAPIContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateCartForCustomerAsync(Customer customer)
        {
            var cart = new Cart
            {
                CustomerId = customer.Id,
                Customer = customer
            };

            _context.Carts.Add(cart);
                return cart;
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }
        

        public async Task<Cart?> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Carts
            .Include(c => c.Items)          
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await SaveAsync();      
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
        }

        public CartItem? GetCartItemFromCart(Cart cart, int productId)
        {
            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
            return existingItem;
        }

        public void EmptyCartAsync(Cart cart)
        {
            _context.CartItems.RemoveRange(cart.Items);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetCartWithItemsAsync(int customerId)
        {
            return await _context.Carts
            .Include(c => c.Items)          
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }


                        
        

    }
}
