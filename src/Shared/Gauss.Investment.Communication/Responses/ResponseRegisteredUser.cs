namespace Gauss.Investment.Communication.Responses
{
    public class ResponseRegisteredUser
    {
        public string Name { get; set; } = string.Empty;
        public ResponseTokens Tokens { get; set; } = default!;
    }
}
