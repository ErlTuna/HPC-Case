using HappenCodeECommerceAPI.Models;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Services
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomerRepository _customerRepo;
        readonly ICartRepository _cartRepo;

        public CustomerService(ICustomerRepository customerRepository, ICartRepository cartRepository)
        {
            _customerRepo = customerRepository;
            _cartRepo = cartRepository;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("Customer cannot be null.", nameof(customer));

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new InvalidOperationException("Customer first name is missing/empty.");
            
            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new InvalidOperationException("Customer last name is missing/empty.");

            if (customer.Age < 18)
                throw new InvalidOperationException("Customer age cannot be less than 18.");

            _customerRepo.Add(customer);
            var cart = await _cartRepo.CreateCartForCustomerAsync(customer);
            customer.CartId = cart.Id;
            customer.Cart = cart;
            await _customerRepo.SaveAsync();

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _customerRepo.GetAllAsync();

            if (customers == null)
                throw new ArgumentNullException("No customers list found.");
           
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer == null)
                throw new ArgumentNullException("No customer found that matches the given id.");

            return customer;
        }

        public async Task UpdateCustomerAsync(int id, Customer updatedCustomer)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer == null)
                throw new ArgumentNullException("No customer found that matches the given id.");

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("Updated customer doesn't have a first name or has an empty first name.", nameof(updatedCustomer.FirstName));

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Customer last name is missing or empty.", nameof(updatedCustomer.LastName));

            if (updatedCustomer.Age < 18)
                throw new ArgumentException("Customer age can not be less than 18.", nameof(updatedCustomer.Age));

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Age = updatedCustomer.Age;
            customer.Address = updatedCustomer.Address;
            customer.Phone = updatedCustomer.Phone;

            await _customerRepo.SaveAsync();
        }
        
        public async Task DeleteCustomerByIdAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer == null)
                throw new ArgumentNullException("No customer found that matches the given id.");

            await _customerRepo.DeleteAsync(customer);
            await _customerRepo.SaveAsync();
        }

    }
}


