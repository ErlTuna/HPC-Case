using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface ICustomerService{
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(int id, Customer updatedCustomer);
        Task DeleteCustomerByIdAsync(int id);
    }
}