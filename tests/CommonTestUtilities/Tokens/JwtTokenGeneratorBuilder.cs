using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Infrastructure.Security.Access.Generator;

namespace CommonTestUtilities.Tokens
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "8B52A5D4-A575-4149-8452-D8303D19DBE7");
    }
}
