using AutoMapper;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Domain.Extensions;
using Gauss.Investment.Domain.Repositories;
using Gauss.Investment.Domain.Services.LoggedUser;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace Gauss.Investment.Application.UseCases.Investment.Register
{
    public class RegisterInvestmentUseCase : IRegisterInvestmentUseCase
    {
        private readonly IInvestmentWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterInvestmentUseCase(
            IInvestmentWriteOnlyRepository repository, 
            ILoggedUser loggedUser, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredInvestment> Execute(RequestInvestment request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var investment = _mapper.Map<Domain.Entities.Investment>(request);
            investment.UserId = loggedUser.Id;

            await _repository.Add(investment);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseResgisteredInvestment>(investment);
        }


        private static void Validate(RequestInvestment request)
        {
            var result = new InvestmentValidator().Validate(request);

            if (result.IsValid.IsFalse()) 
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
