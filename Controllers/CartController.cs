using HappenCodeECommerceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappenCodeECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            try
            {
            var cart = await _cartService.GetCartByCustomerId(customerId);
            return Ok(cart);
            }
            catch (Exception ex)
            {
            return NotFound(ex.Message);
            }
        }



        [HttpPost("{customerId}/add/{productId}")]
        public async Task<IActionResult> AddProduct(int customerId, int productId)
        {
            try
            {
                await _cartService.AddProductAsync(customerId, productId);
                return Ok("Product added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{customerId}/remove/{productId}")]
        public async Task<IActionResult> RemoveProduct(int customerId, int productId)
        {
            try
            {
                await _cartService.RemoveProductAsync(customerId, productId);
                return Ok("Product removed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{customerId}/empty")]
        public async Task<IActionResult> EmptyCart(int customerId)
        {
            try
            {
                await _cartService.EmptyCartByCustomerIdAsync(customerId);
                return Ok("Cart emptied.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
