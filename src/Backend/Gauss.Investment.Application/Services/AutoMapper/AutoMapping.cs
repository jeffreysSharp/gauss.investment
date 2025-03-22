using AutoMapper;
using Gauss.Investment.Communication.Requests;

namespace Gauss.Investment.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, option => option.Ignore());
        }
    }
}
