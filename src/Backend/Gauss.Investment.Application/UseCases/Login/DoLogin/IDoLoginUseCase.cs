using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;

namespace Gauss.Investment.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUser> Execute(RequestLogin request);
    }
}
