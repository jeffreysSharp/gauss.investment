using Gauss.Investment.Domain.Security.Cryptography;
using Gauss.Investment.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter("133865FD-E0A4-40E6-8000-C024D8AB4C7B");
    }
}
