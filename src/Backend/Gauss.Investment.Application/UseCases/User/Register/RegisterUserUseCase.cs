using AutoMapper;
using FluentValidation.Results;
using Gauss.Investment.Application.Cryptography;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Gauss.Investment.Domain.Repositories;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Exceptions;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace Gauss.Investment.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly PasswordEncripter _passwordEncripter;
               
        

        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,            
            IUnitOfWork unitOfWork,
            PasswordEncripter passwordEncripter,
            IAccessTokenGenerator accessTokenGenerator,
            IMapper mapper
            )
        {
            _writeOnlyRepository = writeOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unitOfWork = unitOfWork;           
            _accessTokenGenerator = accessTokenGenerator;            
        }

        public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);                      
            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUser
            {
                Name = user.Name,
                Tokens = new ResponseTokens
                {
                    AccessToken = _accessTokenGenerator.Generete(user.UserIdentifier)
                }
            };
        }

        private async Task Validate(RequestRegisterUser request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExist)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMesssagesException.EMAIL_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}