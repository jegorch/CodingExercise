using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Customer GetCustomerByName(string name);
        Customer GetCustomerById(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByNameAsync(string name);
    }

    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        /// <inheritdoc />
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        #region Implementation of ICustomerRepository

        /// <inheritdoc />
        public Customer GetCustomerByName(string name) { return GetAll().FirstOrDefault(c => c.CustomerName == name); }

        /// <inheritdoc />
        public Customer GetCustomerById(int id) { return GetAll().FirstOrDefault(c => c.Id == id); }

        /// <inheritdoc />
        public async Task<Customer> GetCustomerByIdAsync(int id) { return await GetAll().FirstOrDefaultAsync(c => c.Id == id); }

        /// <inheritdoc />
        public async Task<Customer> GetCustomerByNameAsync(string name) { return await GetAll().FirstOrDefaultAsync(c => c.CustomerName == name); }

        #endregion

        #region Overrides of RepositoryBase<Customer>

        public override IQueryable<Customer> GetAll() { return DbContext.Customers.Include(c => c.AccountType); }

        #endregion

    }
}
