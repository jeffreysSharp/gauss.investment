using Gauss.Investment.Communication.Enums;

namespace Gauss.Investment.Communication.Requests
{
    public class RequestInvestment
    {
        public string Title { get; set; } = string.Empty;
        public InvestmentType? InvestmentType { get; set; }
        public InvestmentCategory? InvestmentCategory { get; set; }
        public IList<InvestmentIssuer> InvestmentIssuers { get; set; } = [];
    }
}
