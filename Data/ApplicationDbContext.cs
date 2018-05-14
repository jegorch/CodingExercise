using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CodingExercise.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[]
        {
                new ConsoleLoggerProvider((category, level)
                                                  => category == DbLoggerCategory.Database.Command.Name
                                                     && level == LogLevel.Error, true)
        });

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<PossibleAnswers> PossibleAnswers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Bundle> Bundles { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<BundleRules> BundleRules { get; set; }
        public virtual DbSet<ProductRules> ProductRules { get; set; }
        public virtual DbSet<ProductBundle> ProductBundle { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            new CustomerConfiguration(builder.Entity<Customer>());
            new SurveyConfiguration(builder.Entity<Survey>());
            new CustomerSurveyConfiguration(builder.Entity<CustomerSurvey>());
            new SurveyQuestionConfiguration(builder.Entity<SurveyQuestion>());
            new BundleConfiguration(builder.Entity<Bundle>());
            new BundleRulesConfiguration(builder.Entity<BundleRules>());
            new ProductRulesConfiguration(builder.Entity<ProductRules>());
            new ProductBundleConfiguration(builder.Entity<ProductBundle>());
        }
    }
}
