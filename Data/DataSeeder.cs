using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingExercise.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            TruncateAllTables(context);

            SeedData(context);
        }

        static void TruncateAllTables(ApplicationDbContext context)
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Answers]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [SurveyQuestion]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ProductRules]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [CustomerSurvey]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [BundleRules]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [BundleProduct]");
            context.Database.ExecuteSqlCommand("DELETE FROM [PossibleAnswers]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Questions]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Survey]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Products]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Customers]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Bundle]");
            context.Database.ExecuteSqlCommand("DELETE FROM [AccountTypes]");
        }

        public static void SeedData(ApplicationDbContext context)
        {
            var survey1 = new Survey
            {
                //Id = Survey.FirstSurvey.Item1,
                Title = Survey.FirstSurvey.Item2,
                Description = Survey.FirstSurvey.Item3
            };
            if (!context.Surveys.Any())
            {
                context.Surveys.Add(survey1);
                context.SaveChanges();
            }

            var acList = new List<AccountType>
            {
                    new AccountType { AccountName = AccountType.CurrentAccount.Value },
                    new AccountType { AccountName = AccountType.CurrentAccountPlus.Value },
                    new AccountType { AccountName = AccountType.StudentAccount.Value },
                    new AccountType { AccountName = AccountType.PensionerAccount.Value }
            };
            context.AddRange(acList);
            context.SaveChanges();

            var qList = new List<Question>
            {
                    new Question {QuestionText = Question.Age.Value},
                    new Question {QuestionText = Question.Student.Value},
                    new Question {QuestionText = Question.Income.Value}
            };
            context.AddRange(qList);
            context.SaveChanges();

            var sqList = new List<SurveyQuestion>
            {
                    new SurveyQuestion{QuestionId = context.Questions.FirstOrDefault(q=>q.QuestionText == Question.Age.Value).Id, SurveyId = survey1.Id},
                    new SurveyQuestion{QuestionId = context.Questions.FirstOrDefault(q=>q.QuestionText == Question.Student.Value).Id, SurveyId = survey1.Id},
                    new SurveyQuestion{QuestionId = context.Questions.FirstOrDefault(q=>q.QuestionText == Question.Income.Value).Id, SurveyId = survey1.Id},
            };
            context.AddRange(sqList);
            context.SaveChanges();

            var questionAge = context.Questions.FirstOrDefault(q => q.QuestionText == Question.Age.Value);
            var questionIsStudent = context.Questions.FirstOrDefault(q => q.QuestionText == Question.Student.Value);
            var questionIncome = context.Questions.FirstOrDefault(q => q.QuestionText == Question.Income.Value);

            var paList = new List<PossibleAnswers>
            {
                    new PossibleAnswers { QuestionId = questionAge.Id, Text = PossibleAnswers.Age_0_17.Value },
                    new PossibleAnswers { QuestionId = questionAge.Id, Text = PossibleAnswers.Age_18_64.Value },
                    new PossibleAnswers { QuestionId = questionAge.Id, Text = PossibleAnswers.Age_65plus.Value },

                    new PossibleAnswers { QuestionId = questionIsStudent.Id, Text = PossibleAnswers.Yes.Value },
                    new PossibleAnswers { QuestionId = questionIsStudent.Id, Text = PossibleAnswers.No.Value },

                    new PossibleAnswers { QuestionId = questionIncome.Id, Text = PossibleAnswers.Income_0.Value },
                    new PossibleAnswers { QuestionId = questionIncome.Id, Text = PossibleAnswers.Income_1_12000.Value },
                    new PossibleAnswers { QuestionId = questionIncome.Id, Text = PossibleAnswers.Income_12001_40000.Value },
                    new PossibleAnswers { QuestionId = questionIncome.Id, Text = PossibleAnswers.Income_40001plus.Value }
            };
            context.AddRange(paList);
            context.SaveChanges();

            var age0_17 = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_0_17.Value);
            var age18_64 = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_18_64.Value);
            var age65plus = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Age_65plus.Value);

            var studYes = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Yes.Value);
            var studNo = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.No.Value);

            var income0 = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_0.Value);
            var income1_12000 = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_1_12000.Value);
            var income12001_40000 = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_12001_40000.Value);
            var income40001plus = context.PossibleAnswers.FirstOrDefault(pa => pa.Text == PossibleAnswers.Income_40001plus.Value);

            #region Current Account Product

            context.Products.Add(new Product { ProductName = Product.CurrentAccount.Value });
            context.SaveChanges();
            var curAccProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.CurrentAccount.Value);

            var currentAccountProductRule1 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = income1_12000.Id };
            var currentAccountProductRule2 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = income12001_40000.Id };
            var currentAccountProductRule3 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = income40001plus.Id };
            var currentAccountProductRule4 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = age18_64.Id };
            var currentAccountProductRule5 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = age65plus.Id };
            var currentAccountProductRule6 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = studNo.Id };
            var currentAccountProductRule7 = new ProductRules { ProductId = curAccProduct.Id, PossibleAnswerId = studYes.Id };
            context.ProductRules.Add(currentAccountProductRule1);
            context.ProductRules.Add(currentAccountProductRule2);
            context.ProductRules.Add(currentAccountProductRule3);
            context.ProductRules.Add(currentAccountProductRule4);
            context.ProductRules.Add(currentAccountProductRule5);
            context.ProductRules.Add(currentAccountProductRule6);
            context.SaveChanges();

            #endregion

            #region Current Account Plus Product

            context.Products.Add(new Product { ProductName = Product.CurrentAccountPlus.Value });
            context.SaveChanges();
            var curAccPlusProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.CurrentAccountPlus.Value);

            var currentAccountPlusProductRule1 = new ProductRules { ProductId = curAccPlusProduct.Id, PossibleAnswerId = income40001plus.Id };
            var currentAccountPlusProductRule2 = new ProductRules { ProductId = curAccPlusProduct.Id, PossibleAnswerId = age18_64.Id };
            var currentAccountPlusProductRule3 = new ProductRules { ProductId = curAccPlusProduct.Id, PossibleAnswerId = age65plus.Id };
            var currentAccountPlusProductRule4 = new ProductRules { ProductId = curAccPlusProduct.Id, PossibleAnswerId = studNo.Id };
            var currentAccountPlusProductRule5 = new ProductRules { ProductId = curAccPlusProduct.Id, PossibleAnswerId = studYes.Id };
            context.ProductRules.Add(currentAccountPlusProductRule1);
            context.ProductRules.Add(currentAccountPlusProductRule2);
            context.ProductRules.Add(currentAccountPlusProductRule3);
            context.ProductRules.Add(currentAccountPlusProductRule4);
            context.SaveChanges();

            #endregion

            #region Junior Saver Account Product

            context.Products.Add(new Product { ProductName = Product.JuniorSaverAccount.Value });
            context.SaveChanges();
            var juniorSaverAccProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.JuniorSaverAccount.Value);

            var juniorSaverAccProductRule1 = new ProductRules { ProductId = juniorSaverAccProduct.Id, PossibleAnswerId = age0_17.Id };
            var juniorSaverAccProductRule2 = new ProductRules { ProductId = juniorSaverAccProduct.Id, PossibleAnswerId = studNo.Id };
            var juniorSaverAccProductRule3 = new ProductRules { ProductId = juniorSaverAccProduct.Id, PossibleAnswerId = income0.Id };
            var juniorSaverAccProductRule4 = new ProductRules { ProductId = juniorSaverAccProduct.Id, PossibleAnswerId = income1_12000.Id };
            var juniorSaverAccProductRule5 = new ProductRules { ProductId = juniorSaverAccProduct.Id, PossibleAnswerId = income12001_40000.Id };
            var juniorSaverAccProductRule6 = new ProductRules { ProductId = juniorSaverAccProduct.Id, PossibleAnswerId = income40001plus.Id };
            context.ProductRules.Add(juniorSaverAccProductRule1);
            context.ProductRules.Add(juniorSaverAccProductRule2);
            context.ProductRules.Add(juniorSaverAccProductRule3);
            context.ProductRules.Add(juniorSaverAccProductRule4);
            context.ProductRules.Add(juniorSaverAccProductRule5);
            context.ProductRules.Add(juniorSaverAccProductRule6);
            context.SaveChanges();

            #endregion

            #region Student Account Product

            context.Products.Add(new Product { ProductName = Product.StudentAccount.Value });
            context.SaveChanges();
            var studentAccProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.StudentAccount.Value);

            var studentSaverAccProductRule1 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = studYes.Id };
            var studentSaverAccProductRule2 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = age18_64.Id };
            var studentSaverAccProductRule3 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = age65plus.Id };
            var studentSaverAccProductRule4 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = income0.Id };
            var studentSaverAccProductRule5 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = income1_12000.Id };
            var studentSaverAccProductRule6 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = income12001_40000.Id };
            var studentSaverAccProductRule7 = new ProductRules { ProductId = studentAccProduct.Id, PossibleAnswerId = income40001plus.Id };
            context.ProductRules.Add(studentSaverAccProductRule1);
            context.ProductRules.Add(studentSaverAccProductRule2);
            context.ProductRules.Add(studentSaverAccProductRule3);
            context.ProductRules.Add(studentSaverAccProductRule4);
            context.ProductRules.Add(studentSaverAccProductRule5);
            context.ProductRules.Add(studentSaverAccProductRule6);
            context.ProductRules.Add(studentSaverAccProductRule7);
            context.SaveChanges();

            #endregion

            #region Debit Card Product

            context.Products.Add(new Product { ProductName = Product.DebitCard.Value });
            context.SaveChanges();
            var debitCardProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.DebitCard.Value);

            var debitCardProductRule1 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = age18_64.Id };
            var debitCardProductRule2 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = age65plus.Id };
            var debitCardProductRule3 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = studNo.Id };
            var debitCardProductRule4 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = studYes.Id };
            var debitCardProductRule5 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = income0.Id };
            var debitCardProductRule6 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = income1_12000.Id };
            var debitCardProductRule7 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = income12001_40000.Id };
            var debitCardProductRule8 = new ProductRules { ProductId = debitCardProduct.Id, PossibleAnswerId = income40001plus.Id };
            context.ProductRules.Add(debitCardProductRule1);
            context.ProductRules.Add(debitCardProductRule2);
            context.ProductRules.Add(debitCardProductRule3);
            context.ProductRules.Add(debitCardProductRule4);
            context.ProductRules.Add(debitCardProductRule5);
            context.ProductRules.Add(debitCardProductRule6);
            context.ProductRules.Add(debitCardProductRule7);
            context.ProductRules.Add(debitCardProductRule8);
            context.SaveChanges();

            #endregion

            #region Credit Card

            context.Products.Add(new Product { ProductName = Product.CreditCard.Value });
            context.SaveChanges();
            var creditCardProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.CreditCard.Value);

            var creditCardProductRule1 = new ProductRules { ProductId = creditCardProduct.Id, PossibleAnswerId = income12001_40000.Id };
            var creditCardProductRule2 = new ProductRules { ProductId = creditCardProduct.Id, PossibleAnswerId = income40001plus.Id };
            var creditCardProductRule3 = new ProductRules { ProductId = creditCardProduct.Id, PossibleAnswerId = age18_64.Id };
            var creditCardProductRule4 = new ProductRules { ProductId = creditCardProduct.Id, PossibleAnswerId = age65plus.Id };
            var creditCardProductRule5 = new ProductRules { ProductId = creditCardProduct.Id, PossibleAnswerId = studNo.Id };
            context.ProductRules.Add(creditCardProductRule1);
            context.ProductRules.Add(creditCardProductRule2);
            context.ProductRules.Add(creditCardProductRule3);
            context.ProductRules.Add(creditCardProductRule4);
            context.ProductRules.Add(creditCardProductRule5);
            context.SaveChanges();

            #endregion

            #region Gold Credit card

            context.Products.Add(new Product { ProductName = Product.GoldCreditCard.Value });
            context.SaveChanges();
            var goldCreditCardProduct = context.Products.FirstOrDefault(p => p.ProductName == Product.GoldCreditCard.Value);

            var goldCreditCardProductRule1 = new ProductRules { ProductId = goldCreditCardProduct.Id, PossibleAnswerId = income40001plus.Id };
            var goldCreditCardProductRule2 = new ProductRules { ProductId = goldCreditCardProduct.Id, PossibleAnswerId = age18_64.Id };
            var goldCreditCardProductRule3 = new ProductRules { ProductId = goldCreditCardProduct.Id, PossibleAnswerId = age65plus.Id };
            var goldCreditCardProductRule4 = new ProductRules { ProductId = goldCreditCardProduct.Id, PossibleAnswerId = studNo.Id };
            context.ProductRules.Add(goldCreditCardProductRule1);
            context.ProductRules.Add(goldCreditCardProductRule2);
            context.ProductRules.Add(goldCreditCardProductRule3);
            context.ProductRules.Add(goldCreditCardProductRule4);
            context.SaveChanges();

            #endregion

            context.SaveChanges();

            #region Junior Saver Bundle

            context.Bundles.Add(new Bundle { BundleName = Bundle.JuniorSaver.Value, Value = 0 });
            context.SaveChanges();
            var juniorSaverBundle = context.Bundles.FirstOrDefault(p => p.BundleName == Bundle.JuniorSaver.Value);

            var juniorSaverBundleRules1 = new BundleRules { BundleId = juniorSaverBundle.Id, PossibleAnswerId = age0_17.Id };
            //Indirect rules
            var juniorSaverBundleRules2 = new BundleRules { BundleId = juniorSaverBundle.Id, PossibleAnswerId = studNo.Id };
            var juniorSaverBundleRules3 = new BundleRules { BundleId = juniorSaverBundle.Id, PossibleAnswerId = income0.Id };
            var juniorSaverBundleRules4 = new BundleRules { BundleId = juniorSaverBundle.Id, PossibleAnswerId = income1_12000.Id };
            var juniorSaverBundleRules5 = new BundleRules { BundleId = juniorSaverBundle.Id, PossibleAnswerId = income12001_40000.Id };
            var juniorSaverBundleRules6 = new BundleRules { BundleId = juniorSaverBundle.Id, PossibleAnswerId = income40001plus.Id };
            context.BundleRules.Add(juniorSaverBundleRules1);
            context.BundleRules.Add(juniorSaverBundleRules2);
            context.BundleRules.Add(juniorSaverBundleRules3);
            context.BundleRules.Add(juniorSaverBundleRules4);
            context.BundleRules.Add(juniorSaverBundleRules5);
            context.BundleRules.Add(juniorSaverBundleRules6);
            var juniorSaverBundleProduct1 = new ProductBundle { BundleId = juniorSaverBundle.Id, ProductId = juniorSaverAccProduct.Id };
            context.ProductBundle.Add(juniorSaverBundleProduct1);
            context.SaveChanges();

            #endregion

            #region Student Bundle

            context.Bundles.Add(new Bundle { BundleName = Bundle.Student.Value, Value = 0 });
            context.SaveChanges();
            var studentBundle = context.Bundles.FirstOrDefault(p => p.BundleName == Bundle.Student.Value);

            var studentBundleRule1 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = studYes.Id };
            var studentBundleRule2 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = age18_64.Id };
            var studentBundleRule3 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = age65plus.Id };
            //Indirect rules
            var studentBundleRule4 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = income0.Id };
            var studentBundleRule5 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = income1_12000.Id };
            var studentBundleRule6 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = income12001_40000.Id };
            var studentBundleRule7 = new BundleRules { BundleId = studentBundle.Id, PossibleAnswerId = income40001plus.Id };
            context.BundleRules.Add(studentBundleRule1);
            context.BundleRules.Add(studentBundleRule2);
            context.BundleRules.Add(studentBundleRule3);
            context.BundleRules.Add(studentBundleRule4);
            context.BundleRules.Add(studentBundleRule5);
            context.BundleRules.Add(studentBundleRule6);
            context.BundleRules.Add(studentBundleRule7);
            var studentBundleProduct1 = new ProductBundle { BundleId = studentBundle.Id, ProductId = studentAccProduct.Id };
            var studentBundleProduct2 = new ProductBundle { BundleId = studentBundle.Id, ProductId = debitCardProduct.Id };
            var studentBundleProduct3 = new ProductBundle { BundleId = studentBundle.Id, ProductId = creditCardProduct.Id };
            context.ProductBundle.Add(studentBundleProduct1);
            context.ProductBundle.Add(studentBundleProduct2);
            context.ProductBundle.Add(studentBundleProduct3);
            context.SaveChanges();

            #endregion

            #region Classic Bundle

            context.Bundles.Add(new Bundle { BundleName = Bundle.Classic.Value, Value = 1 });
            context.SaveChanges();
            var classicBundle = context.Bundles.FirstOrDefault(p => p.BundleName == Bundle.Classic.Value);

            var classicBundleRule1 = new BundleRules { BundleId = classicBundle.Id, PossibleAnswerId = age18_64.Id };
            var classicBundleRule2 = new BundleRules { BundleId = classicBundle.Id, PossibleAnswerId = age65plus.Id };
            var classicBundleRule3 = new BundleRules { BundleId = classicBundle.Id, PossibleAnswerId = income1_12000.Id };
            var classicBundleRule4 = new BundleRules { BundleId = classicBundle.Id, PossibleAnswerId = income12001_40000.Id };
            var classicBundleRule5 = new BundleRules { BundleId = classicBundle.Id, PossibleAnswerId = income40001plus.Id };
            //Indirect rules
            var classicBundleRule6 = new BundleRules { BundleId = classicBundle.Id, PossibleAnswerId = studNo.Id };
            context.BundleRules.Add(classicBundleRule1);
            context.BundleRules.Add(classicBundleRule2);
            context.BundleRules.Add(classicBundleRule3);
            context.BundleRules.Add(classicBundleRule4);
            context.BundleRules.Add(classicBundleRule5);
            context.BundleRules.Add(classicBundleRule6);
            var classicBundleProduct1 = new ProductBundle { BundleId = classicBundle.Id, ProductId = curAccProduct.Id };
            var classicBundleProduct2 = new ProductBundle { BundleId = classicBundle.Id, ProductId = debitCardProduct.Id };
            context.ProductBundle.Add(classicBundleProduct1);
            context.ProductBundle.Add(classicBundleProduct2);
            context.SaveChanges();

            #endregion

            #region Classic Plus Bundle

            context.Bundles.Add(new Bundle { BundleName = Bundle.ClassicPlus.Value, Value = 2 });
            context.SaveChanges();
            var classicPlusBundle = context.Bundles.FirstOrDefault(p => p.BundleName == Bundle.ClassicPlus.Value);

            var classicPlusBundleRule1 = new BundleRules { BundleId = classicPlusBundle.Id, PossibleAnswerId = age18_64.Id };
            var classicPlusBundleRule2 = new BundleRules { BundleId = classicPlusBundle.Id, PossibleAnswerId = age65plus.Id };
            var classicPlusBundleRule3 = new BundleRules { BundleId = classicPlusBundle.Id, PossibleAnswerId = income12001_40000.Id };
            var classicPlusBundleRule4 = new BundleRules { BundleId = classicPlusBundle.Id, PossibleAnswerId = income40001plus.Id };
            //Indirect rules
            var classicPlusBundleRule5 = new BundleRules { BundleId = classicPlusBundle.Id, PossibleAnswerId = studNo.Id };
            context.BundleRules.Add(classicPlusBundleRule1);
            context.BundleRules.Add(classicPlusBundleRule2);
            context.BundleRules.Add(classicPlusBundleRule3);
            context.BundleRules.Add(classicPlusBundleRule4);
            context.BundleRules.Add(classicPlusBundleRule5);
            var classicPlusBundleProduct1 = new ProductBundle { BundleId = classicPlusBundle.Id, ProductId = curAccProduct.Id };
            var classicPlusBundleProduct2 = new ProductBundle { BundleId = classicPlusBundle.Id, ProductId = debitCardProduct.Id };
            var classicPlusBundleProduct3 = new ProductBundle { BundleId = classicPlusBundle.Id, ProductId = creditCardProduct.Id };
            context.ProductBundle.Add(classicPlusBundleProduct1);
            context.ProductBundle.Add(classicPlusBundleProduct2);
            context.ProductBundle.Add(classicPlusBundleProduct3);
            context.SaveChanges();

            #endregion

            #region Gold Bundle

            context.Bundles.Add(new Bundle { BundleName = Bundle.Gold.Value, Value = 3 });
            context.SaveChanges();
            var goldBundle = context.Bundles.FirstOrDefault(p => p.BundleName == Bundle.Gold.Value);

            var goldBundleRule1 = new BundleRules { BundleId = goldBundle.Id, PossibleAnswerId = age18_64.Id };
            var goldBundleRule2 = new BundleRules { BundleId = goldBundle.Id, PossibleAnswerId = age65plus.Id };
            var goldBundleRule3 = new BundleRules { BundleId = goldBundle.Id, PossibleAnswerId = income40001plus.Id };
            var goldBundleRule4 = new BundleRules { BundleId = goldBundle.Id, PossibleAnswerId = studNo.Id };
            context.BundleRules.Add(goldBundleRule1);
            context.BundleRules.Add(goldBundleRule2);
            context.BundleRules.Add(goldBundleRule3);
            context.BundleRules.Add(goldBundleRule4);
            var goldBundleProduct1 = new ProductBundle { BundleId = goldBundle.Id, ProductId = curAccPlusProduct.Id };
            var goldBundleProduct2 = new ProductBundle { BundleId = goldBundle.Id, ProductId = debitCardProduct.Id };
            var goldBundleProduct3 = new ProductBundle { BundleId = goldBundle.Id, ProductId = goldCreditCardProduct.Id };
            context.ProductBundle.Add(goldBundleProduct1);
            context.ProductBundle.Add(goldBundleProduct2);
            context.ProductBundle.Add(goldBundleProduct3);
            context.SaveChanges();

            #endregion

            var customers = new List<Customer>
            {
                    new Customer { CustomerName = "Morpheus", AccountType = context.AccountTypes.FirstOrDefault(a=>a.AccountName == AccountType.CurrentAccount.Value)},
                    new Customer { CustomerName = "Trinity", AccountType = context.AccountTypes.FirstOrDefault(a=>a.AccountName == AccountType.CurrentAccount.Value)},
                    new Customer { CustomerName = "Neo", AccountType = context.AccountTypes.FirstOrDefault(a=>a.AccountName == AccountType.StudentAccount.Value)},
                    new Customer { CustomerName = "Agent Smith", AccountType = context.AccountTypes.FirstOrDefault(a=>a.AccountName == AccountType.PensionerAccount.Value)}
            };
            context.AddRange(customers);
            context.SaveChanges();
        }

    }
}
