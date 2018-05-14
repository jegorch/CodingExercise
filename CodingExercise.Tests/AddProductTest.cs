using System;
using System.Linq;
using CodingExercise.Data;
using CodingExercise.Data.Repositories;
using CodingExercise.Model;
using Xunit;
using Xunit.Abstractions;

namespace CodingExercise.Tests
{
    public class AddProductTest : IClassFixture<DatabaseFixture>
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

        public AddProductTest(DatabaseFixture fixture, ITestOutputHelper output)
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
        public void Junior_Add_JuniorAccount_Test()
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, product, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            //_output.WriteLine($"\tres: [{res}]");
            Assert.Equal("Item allready exists", ex.Message);
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

            var product = _productRepository.GetByName(Product.DebitCard.Value);
            var prodSelection = new ProducSelectionService(_context);

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, product, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            Assert.Equal("Age not hits", ex.Message);
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, card, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            Assert.Equal("Age not hits; Income not hits", ex.Message);
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, card, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            //_output.WriteLine($"\tres: [{res}], Msg: [{msg}]");
            //Assert.False(res);
            Assert.Equal("Stud not hits", ex.Message);
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

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            var card = _productRepository.GetByName(Product.DebitCard.Value);
            var prodSelection = new ProducSelectionService(_context);

            var rem = prodSelection.DelProductFromBundle(recomendeBundle, card, customer, survey);
            _output.WriteLine($"After Remove Bundle: {rem?.BundleName}");
            foreach (var productBundle in rem?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            var res = prodSelection.AddProductToBundle(rem, card, customer, survey);

            _output.WriteLine($"Resulting Bundle: {res?.BundleName}");
            foreach (var productBundle in res?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"\tres: [{res}]");

            Assert.True(res.ProductIncluded.Select(p => p.Product).Contains(card));
            //Assert.Equal("Stud not hits; Income not hits", msg);
        }

        [Fact]
        public void Student_Remove_DebitCard_Test()
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

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            var card = _productRepository.GetByName(Product.DebitCard.Value);
            var prodSelection = new ProducSelectionService(_context);

            var res = prodSelection.DelProductFromBundle(recomendeBundle, card, customer, survey);
            _output.WriteLine($"Resulting Bundle: {res?.BundleName}");
            foreach (var productBundle in res?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            //_output.WriteLine($"\tres: [{res}]");

            Assert.False(res.ProductIncluded.Select(p => p.Product).Contains(card));
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, card, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"Can't Add: {card.ProductName},  reason:[{ex.Message}]");
            Assert.Equal("Stud not hits; Income not hits", ex.Message);
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, card, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"Can't Add: {card.ProductName},  reason:[{ex.Message}]");
            Assert.Equal("Income not hits", ex.Message);
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
            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            var card = _productRepository.GetByName(Product.CreditCard.Value);
            var prodSelection = new ProducSelectionService(_context);

            var rem = prodSelection.DelProductFromBundle(recomendeBundle, card, customer, survey);
            _output.WriteLine($"After Remove Bundle: {rem?.BundleName}");
            foreach (var productBundle in rem?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            var res = prodSelection.AddProductToBundle(rem, card, customer, survey);

            _output.WriteLine($"Resulting Bundle: {res?.BundleName}");
            foreach (var productBundle in res?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }

            Assert.True(res.ProductIncluded.Select(p => p.Product).Contains(card));
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, card, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"Can't Add: [{card.ProductName}],  reason:[{ex.Message}]");
            Assert.Equal("Stud not hits", ex.Message);
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

            var ex = Assert.Throws<CantAddExcption>(() => prodSelection.AddProductToBundle(recomendeBundle, card, customer, survey));

            _output.WriteLine($"Recomended Bundle: {recomendeBundle?.BundleName} \tValue: {recomendeBundle?.Value}");
            foreach (var productBundle in recomendeBundle?.ProductIncluded)
            {
                _output.WriteLine($"\tIncluded Product: {productBundle.Product.ProductName}");
            }
            _output.WriteLine($"Can't Add: [{card.ProductName}],  reason:[{ex.Message}]");
            Assert.Equal("Income not hits", ex.Message);
        }

    }
}
