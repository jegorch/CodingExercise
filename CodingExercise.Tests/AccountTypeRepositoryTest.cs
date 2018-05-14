using System;
using System.Linq;
using CodingExercise.Data.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace CodingExercise.Tests
{
    public class AccountTypeRepositoryTest : IClassFixture<DatabaseFixture>
    {
        private readonly ITestOutputHelper _output;

        readonly IAccountTypeRepository _repository;

        public AccountTypeRepositoryTest(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _repository = new AccountTypeRepository(fixture.Context);
        }

        [Fact]
        public void GetAll()
        {
            var allItems = _repository.GetAll();
            _output.WriteLine($"4 = {allItems.Count()}");
            foreach (var item in allItems)
            {
                _output.WriteLine($"\t{item.Id}. {item.AccountName}");
            }
            Assert.Equal(4, allItems.Count());
        }
    }
}
