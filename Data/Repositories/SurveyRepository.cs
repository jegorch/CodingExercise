using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface ISurveyRepository : IRepository<Survey, int>
    {
    }

    public class SurveyRepository : RepositoryBase<Survey>, ISurveyRepository
    {
        /// <inheritdoc />
        public SurveyRepository(ApplicationDbContext context) : base(context) { }

    }
}
