using System;
using System.Collections.Generic;
using CodingExercise.Data;
using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Tests
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase($"{Guid.NewGuid().ToString()}-Test");
            var options = builder.Options;

            Context = new ApplicationDbContext(options);

            DataSeeder.SeedData(Context);
        }

        public ApplicationDbContext Context { get; private set; }

    }
}
