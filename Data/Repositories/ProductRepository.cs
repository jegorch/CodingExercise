using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Product GetByName(string name);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        /// <inheritdoc />
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        #region Implementation of IProductRepository

        /// <inheritdoc />
        public Product GetByName(string name) { return DbContext.Products.FirstOrDefault(p => p.ProductName == name); }

        #endregion
    }
}
