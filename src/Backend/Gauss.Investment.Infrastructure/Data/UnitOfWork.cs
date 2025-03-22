using Gauss.Investment.Domain.Repositories;

namespace Gauss.Investment.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GaussInvestmentDbContext _dbContext;

        public UnitOfWork(GaussInvestmentDbContext dbContext) =>  _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();        
    }
}
