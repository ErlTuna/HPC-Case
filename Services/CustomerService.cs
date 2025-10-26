using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Services
{
    public class CustomerService : ICustomerService
    {
        readonly HappenCodeECommerceAPIContext _context;
        readonly ICustomerRepository _repo;

        public CustomerService(HappenCodeECommerceAPIContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _repo = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "customer cannot be null.");

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("Customer first name is missing/empty.", nameof(customer.FirstName));
            
            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Customer last name is missing/empty.", nameof(customer.LastName));

            if (customer.Age < 18)
                throw new ArgumentException("Customer age cannot be less than 18.", nameof(customer.Age));

            await _repo.AddAsync(customer);
            await _repo.SaveAsync();

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _repo.GetAllAsync();

            if (customers == null)
                throw new ArgumentNullException("No customers list found.");
           
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                throw new ArgumentNullException("No customer found that matches the given id.");

            return customer;
        }

        public async Task UpdateCustomerAsync(int id, Customer updatedCustomer)
        {
            var customer = await _repo.GetByIdAsync(id);
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

            await _repo.SaveAsync();
        }
        
        public async Task DeleteCustomerByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                throw new ArgumentNullException("No customer found that matches the given id.");

            await _repo.DeleteAsync(customer);
        }

    }
}


