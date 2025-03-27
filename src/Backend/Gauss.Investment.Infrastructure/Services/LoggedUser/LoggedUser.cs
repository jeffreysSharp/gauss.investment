using Gauss.Investment.Domain.Entities;
using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Domain.Services.LoggedUser;
using Gauss.Investment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gauss.Investment.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly GaussInvestmentDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(GaussInvestmentDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> User()
        {
            var token = _tokenProvider.Value();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            var identifier =  jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }
    }
}
