using CodingExercise.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingExercise.Data
{
    public class CustomerConfiguration
    {
        public CustomerConfiguration(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.Id).HasName("PK_Customers");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.CustomerName).HasColumnName("CustomerName");

            entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeId");
            entity.HasIndex(e => e.AccountTypeId).HasName("FK_Customer_AccountType");

            entity.HasOne(d => d.AccountType)
                  .WithMany(p => p.Customers)
                  .HasForeignKey(d => d.AccountTypeId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Customers_AccountType");

        }
    }
}
