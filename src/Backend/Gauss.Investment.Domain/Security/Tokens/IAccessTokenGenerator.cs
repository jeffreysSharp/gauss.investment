namespace Gauss.Investment.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generete(Guid userIdentifier);
    }
}
