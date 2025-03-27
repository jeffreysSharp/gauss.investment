using AutoMapper;
using Gauss.Investment.Communication.Responses;
using Gauss.Investment.Domain.Services.LoggedUser;

namespace Gauss.Investment.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseUserProfile> Execute()
        {
            var user = await _loggedUser.User();

            return _mapper.Map<ResponseUserProfile>(user);
        }
    }
}
