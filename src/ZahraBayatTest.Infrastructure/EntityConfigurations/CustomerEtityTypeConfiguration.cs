using AliBayatTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AliBayatTest.Infrastructure.EntityConfigurations
{
    public class CustomerEtityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.Id);

            builder.Property(p => p.Email)
                .HasColumnType("varchar(50)")
                .HasConversion(p => p.Value, p => Email.Create(p).Value);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(p => p.PhoneNumber)
                .HasColumnType("varchar(15)")
                .IsUnicode(true)
                .HasConversion(p => p.Value, p => PhoneNumber.Create(p).Value);

            builder
            .HasIndex(p => new { p.FirstName, p.LastName, p.DateOfBirth })
           .IsUnique(true);
        }
    }
}
