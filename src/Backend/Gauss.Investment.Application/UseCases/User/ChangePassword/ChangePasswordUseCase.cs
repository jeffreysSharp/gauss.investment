using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Domain.Extensions;
using Gauss.Investment.Domain.Repositories;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Domain.Security.Cryptography;
using Gauss.Investment.Domain.Services.LoggedUser;
using Gauss.Investment.Exceptions;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace Gauss.Investment.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;

        public ChangePasswordUseCase(
            ILoggedUser loggedUser, 
            IUserUpdateOnlyRepository repository, 
            IUnitOfWork unitOfWork, 
            IPasswordEncripter passwordEncripter)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
        }

        public async Task Execute(RequestChangePassword request)
        {
            var loggedUser = await _loggedUser.User();

            Validate(request, loggedUser);

            var user = await _repository.GetById(loggedUser.Id);

            user.Password = _passwordEncripter.Encrypt(request.NewPassword);

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestChangePassword request, Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request);

            var currentPasswordfEncripted = _passwordEncripter.Encrypt(request.Password);

            if (currentPasswordfEncripted.Equals(loggedUser.Password).IsFalse())
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMesssagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
