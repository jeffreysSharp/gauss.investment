namespace Gauss.Investment.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : GaussInvestmentException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            ErrorMessages = errorMessages;
        }
    }
}
