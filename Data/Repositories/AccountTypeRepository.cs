using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface IAccountTypeRepository : IRepository<AccountType, int>
    {
        AccountType GetAccountTypeByName(string name);
        AccountType GetAccountTypeById(int id);
        Task<AccountType> GetAccountTypeByIdAsync(int id);
        Task<AccountType> GetAccountTypeByNameAsync(string name);
    }

    public class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
    {
        /// <inheritdoc />
        public AccountTypeRepository(ApplicationDbContext context) : base(context) { }

        #region Implementation of IAccountTypeRepository

        /// <inheritdoc />
        public AccountType GetAccountTypeByName(string name) { return GetAll().FirstOrDefault(c => c.AccountName == name); }

        /// <inheritdoc />
        public AccountType GetAccountTypeById(int id) { return GetAll().FirstOrDefault(c => c.Id == id); }

        /// <inheritdoc />
        public async Task<AccountType> GetAccountTypeByIdAsync(int id) { return await GetAll().FirstOrDefaultAsync(c => c.Id == id); }

        /// <inheritdoc />
        public async Task<AccountType> GetAccountTypeByNameAsync(string name) { return await GetAll().FirstOrDefaultAsync(c => c.AccountName == name); }

        #endregion

    }
}
