using Gauss.Investment.Domain.Entities;
using Gauss.Investment.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Gauss.Investment.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly GaussInvestmentDbContext _dbContext;

        public UserRepository(GaussInvestmentDbContext context) => _dbContext = context;

        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) =>
            await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) =>
            await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier)
            && user.Active);

        public async Task<User?> GetUserByEmailAndPassword(string email, string password) =>        
            await _dbContext
                .Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) 
                && user.Password.Equals(password));

        public  async Task<User> GetById(Guid id)
        {
            return await _dbContext
                .Users
                .FirstAsync(user => user.Id == id);
        }

        public void Update(User user) => _dbContext.Users.Update(user);
    }
}
