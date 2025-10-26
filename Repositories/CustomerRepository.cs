using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Interfaces;

namespace HappenCodeECommerceAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HappenCodeECommerceAPIContext _context;

        public CustomerRepository(HappenCodeECommerceAPIContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }
            

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }
            
        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await SaveAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
