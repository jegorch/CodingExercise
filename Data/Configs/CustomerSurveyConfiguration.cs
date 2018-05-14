using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class CustomerSurveyConfiguration
    {
        public CustomerSurveyConfiguration(EntityTypeBuilder<CustomerSurvey> entity)
        {
            entity.ToTable("CustomerSurvey");
            entity.HasKey(e => new { e.CustomerId, e.SurveyId }).HasName("PK_CustomerSurvey");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

            entity.Property(e => e.SurveyId).HasColumnName("SurveyId");

            entity.Property(e => e.Date).HasColumnName("Date");

        }
    }
}
