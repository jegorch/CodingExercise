using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface IBundleRepository : IRepository<Bundle, int>
    {
    }

    public class BundleRepository : RepositoryBase<Bundle>, IBundleRepository
    {
        /// <inheritdoc />
        public BundleRepository(ApplicationDbContext context) : base(context) { }

        #region Overrides of RepositoryBase<Bundle>

        /// <inheritdoc />
        public override IQueryable<Bundle> GetAll()
        {
            var all = base.GetAll().ToList();
            var allIncl = base.GetAll().Include(b => b.Rules).Include(b => b.ProductIncluded).ToList();
            return DbContext.Bundles.Include(b=>b.Rules).Include(b=>b.ProductIncluded);
        }

        #endregion
    }
}
