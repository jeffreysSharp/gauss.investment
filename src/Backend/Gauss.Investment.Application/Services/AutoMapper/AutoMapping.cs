using AutoMapper;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;

namespace Gauss.Investment.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUser, Domain.Entities.User>()
                .ForMember(dest => dest.Password, option => option.Ignore());

            CreateMap<RequestInvestment, Domain.Entities.Investment>();

        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfile>();
        }
    }
}
