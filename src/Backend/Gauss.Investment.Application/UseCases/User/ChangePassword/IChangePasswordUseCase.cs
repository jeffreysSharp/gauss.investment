using Gauss.Investment.Communication.Requests;

namespace Gauss.Investment.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePassword request);
    }
}
