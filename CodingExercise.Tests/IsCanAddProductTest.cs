using System;
using System.Linq;
using CodingExercise.Data;
using CodingExercise.Data.Repositories;
using CodingExercise.Model;
using Xunit;
using Xunit.Abstractions;

namespace CodingExercise.Tests
{
    public class IsCanAddProductTest : IClassFixture<DatabaseFixture>
    {
        private readonly ITestOutputHelper _output;

        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBundleRepository _bundleRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ICustomerSurveyRepository _customerSurveyRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IPossibleAnswersRepository _possibleAnswersRepository;
        private readonly ApplicationDbContext _context;

        public IsCanAddProductTest(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _context = fixture.Context;
            _customerRepository = new CustomerRepository(fixture.Context);
            _accountTypeRepository = new AccountTypeRepository(fixture.Context);
            _surveyRepository = new SurveyRepository(fixture.Context);
            _productRepository = new ProductRepository(fixture.Context);
            _bundleRepository = new BundleRepository(fixture.Context);
            _questionRepository = new QuestionRepository(fixture.Context);
            _customerSurveyRepository = new CustomerSurveyRepository(fixture.Context);
            _answerRepository = new AnswerRepository(fixture.Context);
            _possibleAnswersRepository = new PossibleAnswersRepository(fixture.Context);
        }

        [Fact]
        public void Junior_CanAdd_JuniorAccount_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Junior2" };
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

            var product = _productRepository.GetByName(Product.JuniorSaverAccount.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(product, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.True(res);
            //Assert.Equal("Stud not hits", msg);
        }

        [Fact]
        public void Junior_CantAdd_DebitCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Junior2" };
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

            var debitCard = _productRepository.GetByName(Product.DebitCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(debitCard, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
            Assert.Equal("Age not hits", msg);
        }

        [Fact]
        public void Junior_CantAdd_CreditCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Junior3" };
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

            var card = _productRepository.GetByName(Product.CreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
            Assert.Equal("Age not hits; Income not hits", msg);
        }

        [Fact]
        public void Student_CantAdd_CreditCard_WithBigIncome_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Student adds credit card, income 12001-40000" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Yes.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.CreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
            //Assert.Equal("Stud not hits; Income not hits", msg);
        }

        [Fact]
        public void Student_CanAdd_DebitCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Student adds credit card, income 12001-40000" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Yes.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.DebitCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.True(res);
            //Assert.Equal("Stud not hits; Income not hits", msg);
        }

        [Fact]
        public void Student_CantAdd_CreditCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Student adds credit card" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Yes.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_0.Value);//"0"
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.CreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
            //Assert.Equal("Stud not hits; Income not hits", msg);
        }

        [Fact]
        public void Classic_CantAdd_CreditCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Customer adds credit card" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_1_12000.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.CreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
            Assert.Equal("Income not hits", msg);
        }

        [Fact]
        public void Classic_CanAdd_CreditCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Customer adds credit card" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.CreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.True(res);
        }

        [Fact]
        public void Classic_CantAdd_StudentAcc_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Customer adds StudentAccount" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.StudentAccount.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
        }

        [Fact]
        public void Classic_CantAdd_GoldCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Customer adds Gold card" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.GoldCreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.False(res);
        }

        [Fact]
        public void Gold_CanAdd_GoldCard_Test()
        {
            var customer = new Customer { AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value), CustomerName = "Customer adds Gold card" };
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
            var selectedAge = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var answerAge = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = ageQuestion.Id, SelectedAnswerId = selectedAge.Id };
            _answerRepository.Add(answerAge);
            var studQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Student.Value); ;//Is Student?
            var selectedStud = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);
            var answerStud = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = studQuestion.Id, SelectedAnswerId = selectedStud.Id };
            _answerRepository.Add(answerStud);
            var incomeQuestion = _questionRepository.GetAll().FirstOrDefault(q => q.QuestionText == Question.Income.Value);//Income?
            var selectedIncome = _possibleAnswersRepository.GetAll().FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_40001plus.Value);
            var answerIncome = new Answer { CustomerId = customer.Id, SurveyId = survey.Id, QuestionId = incomeQuestion.Id, SelectedAnswerId = selectedIncome.Id };
            _answerRepository.Add(answerIncome);

            var bundleRecomemndation = new BundleRecomendationService(_context);
            Bundle recomendeBundle = bundleRecomemndation.Recommend(customer, survey);

            var card = _productRepository.GetByName(Product.GoldCreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);
            string msg;
            var res = prodSelection.IsCanAddProduct(card, customer, survey, out msg);

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            Assert.True(res);
        }

    }
}
