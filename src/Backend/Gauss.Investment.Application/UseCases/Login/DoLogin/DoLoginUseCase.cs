using Gauss.Investment.Application.Cryptography;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace Gauss.Investment.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncripter _passwordEncripter;

        public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncripter)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
            var user = await _repository.GetUserByEmailAndPassword(request.Email, encriptedPassword) 
                ?? throw new InvalidLoginException();

            return new ResponseRegisteredUser
            {
                Name = user.Name,
            };
        }
    }
}
