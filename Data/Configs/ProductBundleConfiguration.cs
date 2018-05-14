using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class ProductBundleConfiguration
    {
        public ProductBundleConfiguration(EntityTypeBuilder<ProductBundle> entity)
        {
            entity.ToTable("BundleProduct");
            entity.HasKey(e => new { e.BundleId, e.ProductId }).HasName("PK_BundleProduct");
            entity.Property(e => e.BundleId).HasColumnName("BundleId");

            entity.Property(e => e.ProductId).HasColumnName("ProductId");


            entity.HasOne(d => d.Bundle)
                  .WithMany(p => p.ProductIncluded)
                  .HasForeignKey(d => d.BundleId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_BundleProduct_Bundle");

            entity.HasOne(d => d.Product)
                  .WithMany(p => p.Bundles)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_BundleProduct_Product");

        }
    }
}
