using CodingExercise.Data;
using CodingExercise.Data.Repositories;
using CodingExercise.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CodingExercise.Tests
{
    public class CustomeRepositoryTest : IClassFixture<DatabaseFixture>
    {
        private readonly ITestOutputHelper _output;

        readonly ICustomerRepository _customerRepository;
        readonly IAccountTypeRepository _accountTypeRepository;
        private readonly ApplicationDbContext _context;

        public CustomeRepositoryTest(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _context = fixture.Context;
            _customerRepository = new CustomerRepository(fixture.Context);
            _accountTypeRepository = new AccountTypeRepository(fixture.Context);
        }

        [Fact]
        public void GetAll()
        {
            var allItems = _customerRepository.GetAll();
            var cnt = _context.Customers.Count();
            _output.WriteLine($"{cnt} = {allItems.Count()}");
            foreach (var item in allItems)
            {
                _output.WriteLine($"\t{item.Id}. {item.CustomerName}");
            }
            Assert.Equal(cnt, allItems.Count());
            allItems.Count().Should().Be(cnt);
        }

        [Fact]
        public void GetById()
        {
            var morf = _context.Customers.FirstOrDefault(c => c.CustomerName == "Morpheus");
            var items = _customerRepository.GetCustomerById(morf.Id);
            _output.WriteLine($"{morf.Id} = {items?.Id}");
            _output.WriteLine($"{morf.CustomerName} = {items?.CustomerName}");
            _output.WriteLine($"{morf.AccountTypeId} = {items?.AccountTypeId}");

            Assert.Equal(morf.Id, items?.Id);
            Assert.Equal(morf.CustomerName, items?.CustomerName);
            Assert.Equal(morf.AccountTypeId, items?.AccountTypeId);
        }

        [Fact]
        public void GetByName()
        {
            var items = _customerRepository.GetCustomerByName("Neo");
            var neo = _context.Customers.FirstOrDefault(c => c.CustomerName == "Neo");
            Assert.Equal(neo.Id, items.Id);
            Assert.Equal("Neo", items.CustomerName);
            Assert.Equal(neo.AccountTypeId, items.AccountTypeId);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            var morf = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == "Morpheus");
            var items = await _customerRepository.GetCustomerByIdAsync(morf.Id);
            _output.WriteLine($"{morf.Id} = {items?.Id}");
            _output.WriteLine($"Morpheus = {items?.CustomerName}");
            _output.WriteLine($"{morf.AccountTypeId} = {items?.AccountTypeId}");

            Assert.Equal(morf.Id, items.Id);
            Assert.Equal("Morpheus", items.CustomerName);
            Assert.Equal(morf.AccountTypeId, items.AccountTypeId);
        }

        [Fact]
        public async Task GetByNameAsync()
        {
            var items = await _customerRepository.GetCustomerByNameAsync("Neo");
            var neo = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == "Neo");
            Assert.Equal(neo.Id, items?.Id);
            Assert.Equal("Neo", items.CustomerName);
            Assert.Equal(neo.AccountTypeId, items.AccountTypeId);
        }

        [Fact]
        public void Add()
        {
            var itemsCnt = _customerRepository.GetAll().Count();
            var dozer = new Customer { CustomerName = "Apoc", AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value) };
            _customerRepository.Add(dozer);

            var item = _customerRepository.GetCustomerByName("Apoc");
            Assert.Equal("Apoc", item.CustomerName);
        }

        [Fact]
        public async Task AddAsync()
        {
            var itemsCnt = _customerRepository.GetAll().Count();
            var dozer = new Customer { CustomerName = "Dozer", AccountType = _accountTypeRepository.GetAccountTypeByName(AccountType.CurrentAccount.Value) };
            await _customerRepository.AddAsync(dozer);

            var items = _customerRepository.GetAll();
            Assert.Equal(itemsCnt + 1, await items.CountAsync());

            var item = _customerRepository.GetCustomerByName("Dozer");
            Assert.Equal("Dozer", item.CustomerName);
        }

    }
}
