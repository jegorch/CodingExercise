using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CodingExercise.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        #region Implementation of IDesignTimeDbContextFactory<out ApplicationDbContext>

        /// <inheritdoc />
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(
                                 "Server=(localdb)\\mssqllocaldb;Database=aspnet-CodingExercise-CEB557BF-912A-4383-AC47-F431680E8CC8;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDbContext(builder.Options);
        }

        #endregion
    }
}
