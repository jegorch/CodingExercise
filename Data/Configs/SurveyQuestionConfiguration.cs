using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class SurveyQuestionConfiguration
    {
        public SurveyQuestionConfiguration(EntityTypeBuilder<SurveyQuestion> entity)
        {
            entity.ToTable("SurveyQuestion");
            entity.HasKey(e => new { e.SurveyId, e.QuestionId }).HasName("PK_SurveyQuestion");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionId");

            entity.Property(e => e.SurveyId).HasColumnName("SurveyId");

        }
    }
}
