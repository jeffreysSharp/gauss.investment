using Bogus;
using Gauss.Investment.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestChangePasswordBuilder
    {
        public static RequestChangePassword Build(int passwordLength = 10)
        {
            return new Faker<RequestChangePassword>()
                .RuleFor(u => u.Password, (f) => f.Internet.Password())
                .RuleFor(u => u.NewPassword, (f) => f.Internet.Password(passwordLength));
        }
    }
}
