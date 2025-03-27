using Gauss.Investment.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gauss.Investment.Infrastructure.Security.Access.Validator
{
    public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _signingKey;

        public JwtTokenValidator(string signingKey) => _signingKey = signingKey;
        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var valitationParameter = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_signingKey),
                ClockSkew = new TimeSpan(0)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, valitationParameter, out _);
            var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            return Guid.Parse(userIdentifier);
        }
    }
}
