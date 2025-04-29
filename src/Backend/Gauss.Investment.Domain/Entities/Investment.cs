namespace Gauss.Investment.Domain.Entities
{
    public class Investment : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public IList<InvestmentType> InvestmentTypes { get; set; } = [];
        public IList<InvestmentCategory> InvestmentCategories { get; set; } = [];
        public IList<InvestmentIssuer> InvestmentIssuers { get; set; } = [];
        public Guid UserId { get; set; }
    }
}
