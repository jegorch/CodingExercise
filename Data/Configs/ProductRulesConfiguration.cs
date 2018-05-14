using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class ProductRulesConfiguration
    {
        public ProductRulesConfiguration(EntityTypeBuilder<ProductRules> entity)
        {
            entity.ToTable("ProductRules");
            entity.HasKey(e => new { e.ProductId, e.PossibleAnswerId }).HasName("PK_ProductRules");
            entity.Property(e => e.ProductId).HasColumnName("ProductId");

            entity.Property(e => e.PossibleAnswerId).HasColumnName("PossibleAnswerId");


            entity.HasOne(d => d.Product)
                  .WithMany(p => p.Rules)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_ProductRules_Bundle");

            entity.HasOne(d => d.PossibleAnswer)
                  .WithMany(p => p.ProductPossibleAnswers)
                  .HasForeignKey(d => d.PossibleAnswerId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_ProductRules_PossibleAnswers");

        }
    }
}
