using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;

namespace Gauss.Investment.Application.UseCases.User.Register
{
    public interface IRegisterUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
