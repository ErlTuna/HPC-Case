using Microsoft.AspNetCore.Mvc;
using HappenCodeECommerceAPI.Models;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = createdProduct.Id },
                    createdProduct);
            }

            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            try
            {
                await _productService.UpdateProductAsync(id, updatedProduct);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductByIdAsync(id);
                return NoContent();
            }
            
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}


// Some notes for myself
// [ApiController] property is used to denote that this class will handle web API requests
// This enables the use of :
// 1) Automatic Model Validation -> if model is invalid, returns 400 Bad Request
// 2) Automatic Parameter Binding -> Infers where parameters should come from
// func_name(Product product) -> .NETCore reads the JSON and converts it into a Product object
// 3) Automatic Error Response -> Invalid or missing parameters produce error messages
// 
// Furthermore, it standardizes responses to errors (i.e parameter missing? returns 400 responses)

// [Route] -> defines the base URL path that clients will use to access this controller’s endpoints.
// [Route"(api/controller)"] -> controller is replaced by the class name minus controller.
// [Route"(api/controller)"] becomes /api/products 
// This defines the basepath.

// While ACTIONS themselves such as HttpPost, HttpGet, HttpPut, HttpDelete define specific endpoints
// For example : "For an HttpGet request, use this method."
// For example, getting a Product via its id would produce the path : /api/products/<product_id>
// Getting all products -> /api/products
// Creating a product -> /api/products

// Don't forget to use CreatedAtAction for creating new Model objects.