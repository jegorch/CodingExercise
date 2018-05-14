using System;
using System.Linq;
using CodingExercise.Data;
using CodingExercise.Data.Repositories;
using CodingExercise.Model;
using Xunit;
using Xunit.Abstractions;

namespace CodingExercise.Tests
{
    public class RecomendedBundleTest : IClassFixture<DatabaseFixture>
    {
        private readonly ITestOutputHelper _output;

        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly ISurveyRepository _surveyRepository;
        //private readonly IProductRepository _productRepository;
        //private readonly IBundleRepository _bundleRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ICustomerSurveyRepository _customerSurveyRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IPossibleAnswersRepository _possibleAnswersRepository;
        private readonly ApplicationDbContext _context;

        public RecomendedBundleTest(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Start RecomendedBundle Tests");
            _context = fixture.Context;
            _customerRepository = new CustomerRepository(fixture.Context);
            _accountTypeRepository = new AccountTypeRepository(fixture.Context);
            _surveyRepository = new SurveyRepository(fixture.Context);
            //_productRepository = new ProductRepository(fixture.Context);
            //_bundleRepository = new BundleRepository(fixture.Context);
            _questionRepository = new QuestionRepository(fixture.Context);
            _customerSurveyRepository = new CustomerSurveyRepository(fixture.Context);
            _answerRepository = new AnswerRepository(fixture.Context);
            _possibleAnswersRepository = new PossibleAnswersRepository(fixture.Context);
        }

        [Fact]
        public void JuniorTest()
        {
            var customer = new Customer
            {
                //Id = 99,
                AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value),
                CustomerName = "Junior1"
            };
            _customerRepository.Add(customer);
            var survey = _surveyRepository.GetAll().FirstOrDefault(s => s.Title == Survey.FirstSurvey.Item2);
            var custSurv = new CustomerSurvey
            {
                CustomerId = customer.Id,
                SurveyId = survey.Id,
                Date = DateTime.Now
            };
            _customerSurveyRepository.Add(custSurv);
            var ageQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Age.Value);//Age?
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_0_17.Value);//"0-17"
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);//"no"
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_0.Value);//"0"
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} Value: {recomendeBundle?.Value}");
            Assert.Equal(Bundle.JuniorSaver.Value, recomendeBundle?.BundleName);
            _output.WriteLine($"\tIncluded Product: {recomendeBundle?.ProductIncluded?.FirstOrDefault().Product.ProductName}");
            Assert.Equal(Product.JuniorSaverAccount.Value, recomendeBundle?.ProductIncluded?.FirstOrDefault().Product.ProductName);
        }

        [Fact]
        public void StudentTest()
        {
            var student = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.StudentAccount.Value), CustomerName = "Student" };
            _customerRepository.Add(student);
            var survey = _surveyRepository.GetAll().FirstOrDefault(s => s.Title == Survey.FirstSurvey.Item2);
            var custSurv = new CustomerSurvey
            {
                CustomerId = student.Id,
                SurveyId = survey.Id,
                Date = DateTime.Now
            };
            _customerSurveyRepository.Add(custSurv);
            var ageQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Age.Value);//Age?
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);//"18-64"
            var answerAge = new Answer { CustomerId = student.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Yes.Value);//"Yes"
            var answerStud = new Answer { CustomerId = student.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_1_12000.Value);//"1-12000"
            var answerIncome = new Answer { CustomerId = student.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(student, survey);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} Value: {recomendeBundle?.Value}");
            Assert.Equal("Student", recomendeBundle?.BundleName);
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
        }

        [Fact]
        public void _12kIncomeTest()
        {
            var customerName = "Customer with 12k income";
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeById(1), CustomerName = customerName };
            _customerRepository.Add(customer);
            var survey = _surveyRepository.GetAll().FirstOrDefault(s => s.Title == Survey.FirstSurvey.Item2);
            var custSurv = new CustomerSurvey
            {
                CustomerId = customer.Id,
                SurveyId = survey.Id,
                Date = DateTime.Now
            };
            _customerSurveyRepository.Add(custSurv);
            var ageQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Age.Value);//Age?
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);//"18-64"
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);//"no"
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_1_12000.Value);//"1-12000"
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} Value: {recomendeBundle?.Value}");
            Assert.Equal("Classic", recomendeBundle?.BundleName);
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
        }

        [Fact]
        public void more12kIncomeTest()
        {
            var customerName = "Customer with more than 12k income";
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeById(1), CustomerName = customerName };
            _customerRepository.Add(customer);
            var survey = _surveyRepository.GetAll().FirstOrDefault(s => s.Title == Survey.FirstSurvey.Item2);
            var custSurv = new CustomerSurvey
            {
                CustomerId = customer.Id,
                SurveyId = survey.Id,
                Date = DateTime.Now
            };
            _customerSurveyRepository.Add(custSurv);
            var ageQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Age.Value);//Age?
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);//"18-64"
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);//"no"
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);//"12001-40000"
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} Value: {recomendeBundle?.Value}");
            Assert.Equal("Classic Plus", recomendeBundle?.BundleName);
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
        }

        [Fact]
        public void more40kIncomeTest()
        {
            var customerName = "Customer with more than 40k income";
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeById(1), CustomerName = customerName };
            _customerRepository.Add(customer);
            var survey = _surveyRepository.GetAll().FirstOrDefault(s => s.Title == Survey.FirstSurvey.Item2);
            var custSurv = new CustomerSurvey
            {
                CustomerId = customer.Id,
                SurveyId = survey.Id,
                Date = DateTime.Now
            };
            _customerSurveyRepository.Add(custSurv);
            var ageQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Age.Value);//Age?
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);//"18-64"
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);//"no"
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_40001plus.Value);//"40000+"
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} Value: {recomendeBundle?.Value}");
            Assert.Equal("Gold", recomendeBundle?.BundleName);
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
        }
    }
}
