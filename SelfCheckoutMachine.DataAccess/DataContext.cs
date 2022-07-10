using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.Entities;

namespace SelfCheckoutMachine.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().ToTable("Currency");
            modelBuilder.Entity<Currency>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Currency>().Property(p => p.Bill).IsRequired();
            modelBuilder.Entity<Currency>().Property(p => p.ValueInHuf).IsRequired();
            modelBuilder.Entity<Currency>().Property(p => p.ValueInEur).IsRequired(); 
            modelBuilder.Entity<Currency>().Property(p => p.Amount).IsRequired();
        }
    }
}