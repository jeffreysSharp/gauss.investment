namespace Gauss.Investment.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : GaussInvestmentException
    {
        public InvalidLoginException() : base(ResourceMesssagesException.EMAIL_OR_PASSWORD_INVALID)
        {
        }
    }
}
