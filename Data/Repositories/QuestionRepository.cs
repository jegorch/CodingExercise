using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question, int>
    {
        Question GetQuestionByName(string name);
    }

    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        /// <inheritdoc />
        public QuestionRepository(ApplicationDbContext context) : base(context) { }

        #region Implementation of IQuestionRepository

        /// <inheritdoc />
        public Question GetQuestionByName(string name) { return GetAll().FirstOrDefault(q => q.QuestionText == name); }

        #endregion
    }
}
