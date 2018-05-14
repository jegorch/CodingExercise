using System.Collections.Generic;
using System.Linq;
using CodingExercise.Data.Repositories;
using CodingExercise.Model;

namespace CodingExercise.Data
{
    public class BundleRecomendationService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ApplicationDbContext _context;

        public BundleRecomendationService(ApplicationDbContext context)
        {
            _context = context;
            _bundleRepository = new BundleRepository(context);
            _answerRepository = new AnswerRepository(context);
            _questionRepository = new QuestionRepository(context);
        }

        public Bundle Recommend(Customer customer, Survey survey)
        {
            var answers = _answerRepository.GetAll().Where(a => a.CustomerId == customer.Id && a.SurveyId == survey.Id);
            var age = answers.FirstOrDefault(a => a.QuestionId == _questionRepository.GetQuestionByName(Question.Age.Value).Id)?.SelectedAnswer;
            var isStud = answers.FirstOrDefault(a => a.QuestionId == _questionRepository.GetQuestionByName(Question.Student.Value).Id)?.SelectedAnswer;
            var income = answers.FirstOrDefault(a => a.QuestionId == _questionRepository.GetQuestionByName(Question.Income.Value).Id)?.SelectedAnswer;

            #region DEBUG

            var allbundle = _bundleRepository.GetAll().ToList();
            var allbundleCnx = _context.Bundles.ToList();
            var ageOnly = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(age.QuestionId)).ToList();
            var ageOnly1 = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id)).ToList();
            List<Bundle> studOnly = null;
            List<Bundle> incomeOnly = null;
            if (isStud != null)
            {
                studOnly = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(isStud.Id)).ToList();
            }
            if (income != null)
            {
                incomeOnly = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(income.Id)).ToList();
            }

            #endregion

            var bundleList = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id)).ToList();
            if (isStud != null)
                bundleList = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id) & b.Rules.Select(r => r.PossibleAnswerId).Contains(isStud.Id)).ToList();
            if (income != null)
                bundleList = _bundleRepository.GetAll().Where(b => b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id) & b.Rules.Select(r => r.PossibleAnswerId).Contains(isStud.Id) & b.Rules.Select(r => r.PossibleAnswerId).Contains(income.Id)).ToList();
            Bundle recommendedBundle = null;
            if (bundleList.Any())
                if (bundleList.Count == 1)
                    return bundleList.FirstOrDefault();
                else
                    recommendedBundle = bundleList.Aggregate((i1, i2) => i1.Value > i2.Value ? i1 : i2);
            return recommendedBundle;
        }
    }
}
