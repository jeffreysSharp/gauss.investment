using Gauss.Investment.Communication.Enums;

namespace Gauss.Investment.Communication.Requests
{
    public class RequestInvestment
    {
        public string Title { get; set; } = string.Empty;
        public InvestmentType? InvestmentTypes { get; set; }
        public InvestmentCategory? InvestmentCategories { get; set; }
        public IList<InvestmentIssuer> InvestmentIssuers { get; set; } = [];
    }
}
