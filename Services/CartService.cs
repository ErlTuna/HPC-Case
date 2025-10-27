using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Services
{
    public class CartService : ICartService
    {
        readonly HappenCodeECommerceAPIContext _context;
        readonly ICartRepository _cartRepo;
        readonly ICustomerRepository _customerRepo;
        readonly IProductRepository _productRepo;

        public CartService(HappenCodeECommerceAPIContext context, ICartRepository cartRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _context = context;
            _cartRepo = cartRepository;
            _customerRepo = customerRepository;
            _productRepo = productRepository;
        }

        public async Task<Cart> CreateCartForCustomerAsync(Customer customer)
        {
            // just in case the customer already has a cart
            var existingCart = await _cartRepo.GetByCustomerIdAsync(customer.Id);
            if (existingCart != null)
                return existingCart;

            var cart = new Cart
            {
                CustomerId = customer.Id,
                Customer = customer
            };

            _context.Carts.Add(cart);
            await _cartRepo.SaveAsync();

            return cart;
        }

        public async Task AddProductAsync(int customerId, int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null)
                throw new ArgumentNullException("Product with given id was not found.", nameof(productId));

            var customer = await _customerRepo.GetByIdAsync(customerId);
            if (customer == null)
                throw new ArgumentNullException("Given customer does not exist.", nameof(customerId));

            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);
            if (cart == null)
                throw new ArgumentNullException("Cart not found for given customer.", nameof(customerId));


            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Product = product,
                    Quantity = 1
                });
            }

            await _cartRepo.SaveAsync();

        }

        public async Task EmptyCartByCustomerIdAsync(int customerId)
        {
        
            // GetCartByCustomerId already throws
            var cart = await GetCartByCustomerId(customerId);
            _cartRepo.EmptyCartAsync(cart);
            await _cartRepo.SaveAsync();
            
        }

        public async Task<Cart> GetCartByCustomerId(int customerId)
        {
            var cart = await _cartRepo.GetCartWithItemsAsync(customerId);
            if (cart == null)
                throw new ArgumentNullException("Cart not found for given customer.");

            return cart;
        }

        public async Task<Cart> GetCartById(int id)
        {
            var cart = await _cartRepo.GetByIdAsync(id);
            if (cart == null)
                throw new ArgumentNullException("Cart not found for given customer.");

            return cart;
        }
        
        public async Task RemoveProductAsync(int customerId, int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null)
                throw new ArgumentNullException("No product with given id exists.", nameof(productId));

            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);
            if (cart == null)
                throw new ArgumentNullException("Cart not found for given customer.");

            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
                throw new ArgumentException("Product not found in cart.");

            cartItem.Quantity--;
            if (cartItem.Quantity <= 0)
                _cartRepo.RemoveCartItem(cartItem);

            await _cartRepo.SaveAsync();

        }

        
    }
    

}


