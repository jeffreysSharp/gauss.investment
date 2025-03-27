using Gauss.Investment.Communication.Requests;

namespace Gauss.Investment.Application.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task Execute(RequestUpdateUser request);
    }
}
