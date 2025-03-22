using Gauss.Investment.Domain.Entities;
using Gauss.Investment.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Gauss.Investment.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly GaussInvestmentDbContext _context;

        public UserRepository(GaussInvestmentDbContext context) => _context = context;

        public async Task Add(User user) => await _context.Users.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) =>        
            await _context.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);        
    }
}
