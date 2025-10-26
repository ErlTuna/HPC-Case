using HappenCodeECommerceAPI.Models;

namespace HappenCodeECommerceAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer?> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<bool> DeleteAsync(Customer customer);
        Task SaveAsync();
    }
}