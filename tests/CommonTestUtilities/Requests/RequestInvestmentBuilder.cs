using Bogus;
using Gauss.Investment.Communication.Enums;
using Gauss.Investment.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestInvestmentBuilder
    {
        public static RequestInvestment Build()
        {
            return new Faker<RequestInvestment>()
                .RuleFor(i => i.Title, f => f.Lorem.Word())
                .RuleFor(i => i.InvestmentCategory, f => f.PickRandom<InvestmentCategory>())
                .RuleFor(i => i.InvestmentType, f => f.PickRandom<InvestmentType>());
        }
    }
}
