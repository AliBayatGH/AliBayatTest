using Microsoft.EntityFrameworkCore;
using AliBayatTest.Domain;
using AliBayatTest.Infrastructure.EntityConfigurations;

namespace AliBayatTest.Infrastructure
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext()
        {

        }
        public ApplicationDbcontext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEtityTypeConfiguration());
        }
    }
}
