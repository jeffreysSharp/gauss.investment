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

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) =>
            await _context.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier)
            && user.Active);

        public async Task<User?> GetUserByEmailAndPassword(string email, string password) =>        
            await _context
                .Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) 
                && user.Password.Equals(password));
        
    }
}
