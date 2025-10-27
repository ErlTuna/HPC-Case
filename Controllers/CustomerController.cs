using Microsoft.AspNetCore.Mvc;
using HappenCodeECommerceAPI.Models;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            try
            {
                var createdCustomer = await _customerService.CreateCustomerAsync(customer);
                return CreatedAtAction(
                    nameof(GetCustomer),
                    new { id = createdCustomer.Id },
                    createdCustomer);
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
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
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
        public async Task<ActionResult<Product>> GetCustomer(int id)
        {
            try
            {
                var product = await _customerService.GetCustomerByIdAsync(id);
                return Ok(product);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, Customer updatedCustomer)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(id, updatedCustomer);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomerByIdAsync(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }

}
