using Gauss.Investment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gauss.Investment.Infrastructure.Data
{
    public class GaussInvestmentDbContext : DbContext
    {
        public GaussInvestmentDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GaussInvestmentDbContext).Assembly);
        }
    }
}
