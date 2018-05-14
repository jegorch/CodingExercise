using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class BundleConfiguration
    {
        public BundleConfiguration(EntityTypeBuilder<Bundle> entity)
        {
            entity.ToTable("Bundle");
            entity.HasKey(e => e.Id).HasName("PK_Bundle");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.BundleName).HasColumnName("BundleName");

            entity.Property(e => e.Value).HasColumnName("Value");
            entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeId");

            entity.HasMany(b => b.Rules).WithOne();
            entity.HasMany(b => b.ProductIncluded).WithOne();

            //entity.HasOne(d => d.AccountType)
            //      .WithMany(p => p.Bundles)
            //      .HasForeignKey(d => d.AccountTypeId)
            //      .OnDelete(DeleteBehavior.Restrict)
            //      .HasConstraintName("FK_Bundle_AccountType");
        }
    }
}
