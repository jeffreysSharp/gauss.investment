using Gauss.Investment.Communication.Responses;

namespace Gauss.Investment.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfile> Execute();
    }
}
