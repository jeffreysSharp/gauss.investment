namespace Gauss.Investment.Domain.Entities
{
    public class InvestmentType : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Guid InvestmentId { get; set; }
    }
}
