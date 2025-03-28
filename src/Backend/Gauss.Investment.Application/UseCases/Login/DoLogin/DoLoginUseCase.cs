using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Domain.Security.Cryptography;
using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace Gauss.Investment.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        
        public DoLoginUseCase(
            IUserReadOnlyRepository repository,
            IAccessTokenGenerator accessTokenGenerator,
            IPasswordEncripter passwordEncripter)
        {
            _repository = repository;            
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
            var user = await _repository.GetUserByEmailAndPassword(request.Email, encriptedPassword) 
                ?? throw new InvalidLoginException();

            return new ResponseRegisteredUser
            {
                Name = user.Name,
                Tokens = new ResponseTokens
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }
    }
}
