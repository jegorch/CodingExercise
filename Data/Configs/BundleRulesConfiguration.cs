using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class BundleRulesConfiguration
    {
        public BundleRulesConfiguration(EntityTypeBuilder<BundleRules> entity)
        {
            entity.ToTable("BundleRules");
            entity.HasKey(e => new { e.BundleId, e.PossibleAnswerId }).HasName("PK_BundleRules");
            entity.Property(e => e.BundleId).HasColumnName("BundleId");

            entity.Property(e => e.PossibleAnswerId).HasColumnName("PossibleAnswerId");


            entity.HasOne(d => d.Bundle)
                  .WithMany(p => p.Rules)
                  .HasForeignKey(d => d.BundleId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_BundleRules_Bundle");

            entity.HasOne(d => d.PossibleAnswer)
                  .WithMany(p => p.BundlePossibleAnswers)
                  .HasForeignKey(d => d.PossibleAnswerId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_BundleRules_PossibleAnswers");

        }
    }
}
