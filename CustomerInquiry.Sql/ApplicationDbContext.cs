using CustomerInquiry.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerInquiry.Sql
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<TransactionEntity> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
                .HasAlternateKey(c => c.Email);
        }
    }
}