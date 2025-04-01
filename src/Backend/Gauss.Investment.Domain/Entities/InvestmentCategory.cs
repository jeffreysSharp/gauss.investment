namespace Gauss.Investment.Domain.Entities
{
    public class InvestmentCategory : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Guid InvestmentId { get; set; }
    }
}
