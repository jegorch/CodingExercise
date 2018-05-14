using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Data.Repositories
{
    public interface IPossibleAnswersRepository : IRepository<PossibleAnswers, int>
    {
        PossibleAnswers GetByName(string name);
    }

    public class PossibleAnswersRepository : RepositoryBase<PossibleAnswers>, IPossibleAnswersRepository
    {
        /// <inheritdoc />
        public PossibleAnswersRepository(ApplicationDbContext context) : base(context) { }

        #region Implementation of IPossibleAnswersRepository

        /// <inheritdoc />
        public PossibleAnswers GetByName(string name) { return DbContext.PossibleAnswers.FirstOrDefault(pa => pa.Text == name); }

        #endregion
    }
}
