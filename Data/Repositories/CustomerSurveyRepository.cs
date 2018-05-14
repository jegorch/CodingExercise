using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface ICustomerSurveyRepository : IRepository<CustomerSurvey, int>
    {
    }

    public class CustomerSurveyRepository : RepositoryBase<CustomerSurvey>, ICustomerSurveyRepository
    {
        /// <inheritdoc />
        public CustomerSurveyRepository(ApplicationDbContext context) : base(context) { }

    }
}
