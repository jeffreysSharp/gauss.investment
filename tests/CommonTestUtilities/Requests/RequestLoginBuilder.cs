using Bogus;
using Gauss.Investment.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestLoginBuilder
    {
        public static RequestLogin Build()
        {
            return new Faker<RequestLogin>()
             .RuleFor(u => u.Email, (f) => f.Internet.Email())
             .RuleFor(u => u.Password, (f) => f.Internet.Password());
        }
    }
}
