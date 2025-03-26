namespace Gauss.Investment.Domain.Secuturity.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generete(Guid userIdentifier);
    }
}
