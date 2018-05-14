using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface IAnswerRepository : IRepository<Answer, int>
    {
    }

    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        /// <inheritdoc />
        public AnswerRepository(ApplicationDbContext context) : base(context) { }

    }
}
