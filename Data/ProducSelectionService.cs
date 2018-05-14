using System;
using System.Linq;
using CodingExercise.Data.Repositories;
using CodingExercise.Model;

namespace CodingExercise.Data
{
    public class ProducSelectionService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBundleRepository _bundleRepository;
        private readonly IAnswerRepository _answerRepository;

        public ProducSelectionService(ApplicationDbContext context)
        {
            _productRepository = new ProductRepository(context);
            _bundleRepository = new BundleRepository(context);
            _answerRepository = new AnswerRepository(context);
        }

        public Bundle AddProductToBundle(Bundle bundle, Product productToAdd, Customer customer, Survey survey)
        {
            string msg;
            var canToAdd = IsCanAddProduct(productToAdd, customer, survey, out msg);
            if (canToAdd)
            {
                if (!bundle.ProductIncluded.Select(p => p.ProductId).Contains(productToAdd.Id))
                {
                    var resBundle = new Bundle();
                    resBundle.Id = 99;
                    resBundle.BundleName = "Result Bundle";
                    resBundle.Value = bundle.Value;

                    foreach (var prd in bundle.ProductIncluded.Select(b => b.Product))
                    {
                        resBundle.ProductIncluded.Add(new ProductBundle { Bundle = resBundle, BundleId = resBundle.Id, Product = prd, ProductId = prd.Id });
                    }
                    resBundle.ProductIncluded.Add(new ProductBundle { Bundle = resBundle, BundleId = resBundle.Id, Product = productToAdd, ProductId = productToAdd.Id });
                    return resBundle;
                }

                throw new CantAddExcption("Item allready exists");
            }
            throw new CantAddExcption(msg);
        }

        public Bundle DelProductFromBundle(Bundle bundle, Product productToRemove, Customer customer, Survey survey)
        {
            //string msg;
            //var canRemove = IsCanAddProduct(productToRemove, customer, survey, out msg);
            //if (canRemove)
            {
                if (bundle.ProductIncluded.Select(p => p.ProductId).Contains(productToRemove.Id))
                {
                    var resBundle = new Bundle();
                    resBundle.Id = 990;
                    resBundle.BundleName = "Result Bundle";
                    resBundle.Value = bundle.Value;

                    foreach (var prd in bundle.ProductIncluded.Select(b => b.Product))
                    {
                        if (prd.Id != productToRemove.Id)
                            resBundle.ProductIncluded.Add(new ProductBundle { Bundle = resBundle, BundleId = resBundle.Id, Product = prd, ProductId = prd.Id });
                    }

                    return resBundle;
                }

                throw new CantAddExcption("Product allready removed");
            }
            //throw new CantAddExcption(msg);
        }

        public bool IsCanAddProduct(Product productToAdd, Customer customer, Survey survey, out string why)
        {
            why = string.Empty;
            var answers = _answerRepository.GetAll().Where(a => a.CustomerId == customer.Id && a.SurveyId == survey.Id);
            var age = answers.FirstOrDefault(a => a.Question.QuestionText == Question.Age.Value)?.SelectedAnswer;
            var stud = answers.FirstOrDefault(a => a.Question.QuestionText == Question.Student.Value)?.SelectedAnswer;
            var income = answers.FirstOrDefault(a => a.Question.QuestionText == Question.Income.Value)?.SelectedAnswer;

            var ruleAge = _productRepository.GetAll().Where(b => b.Id == productToAdd.Id & b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id)).ToList();
            if (!ruleAge.Any())
            {
                var msg = "Age not hits";
                if (string.IsNullOrEmpty(why))
                    why = msg;
                else why += $"; {msg}";
            }
            var ruleStud = _productRepository.GetAll().Where(b => b.Id == productToAdd.Id & b.Rules.Select(r => r.PossibleAnswerId).Contains(stud.Id)).ToList();
            if (!ruleStud.Any())
            {
                var msg = "Stud not hits";
                if (string.IsNullOrEmpty(why))
                    why = msg;
                else why += $"; {msg}";
            }
            var ruleIncome = _productRepository.GetAll().Where(b => b.Id == productToAdd.Id & b.Rules.Select(r => r.PossibleAnswerId).Contains(income.Id)).ToList();
            if (!ruleIncome.Any())
            {
                var msg = "Income not hits";
                if (string.IsNullOrEmpty(why))
                    why = msg;
                else why += $"; {msg}";
            }

            var products = _productRepository.GetAll().Where(b => b.Id == productToAdd.Id & b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id)).ToList();
            if (stud != null)
                products = _productRepository.GetAll().Where(b => b.Id == productToAdd.Id & b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id) & b.Rules.Select(r => r.PossibleAnswerId).Contains(stud.Id)).ToList();
            if (income != null)
                products = _productRepository.GetAll().Where(b => b.Id == productToAdd.Id & b.Rules.Select(r => r.PossibleAnswerId).Contains(age.Id) & b.Rules.Select(r => r.PossibleAnswerId).Contains(stud.Id) & b.Rules.Select(r => r.PossibleAnswerId).Contains(income.Id)).ToList();
            if (products.Any())
            {
                why = string.Empty;
                return true;
            }

            return false;
        }

    }
}
