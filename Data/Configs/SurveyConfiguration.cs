using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class SurveyConfiguration
    {
        public SurveyConfiguration(EntityTypeBuilder<Survey> entity)
        {
            entity.ToTable("Survey");
            entity.HasKey(e => e.Id).HasName("PK_Survey");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.Description).HasColumnName("Description");

            entity.Property(e => e.Title).HasColumnName("Title");

        }
    }
}
