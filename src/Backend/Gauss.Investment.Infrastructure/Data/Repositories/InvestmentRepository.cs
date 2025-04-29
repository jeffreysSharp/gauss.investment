using Gauss.Investment.Domain.Entities;

namespace Gauss.Investment.Infrastructure.Data.Repositories
{
    public sealed class InvestmentRepository : IInvestmentWriteOnlyRepository
    {
        private readonly GaussInvestmentDbContext _dbContext;

        public InvestmentRepository(GaussInvestmentDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Investment investment) => await _dbContext.Investments.AddAsync(investment);
    }
}
