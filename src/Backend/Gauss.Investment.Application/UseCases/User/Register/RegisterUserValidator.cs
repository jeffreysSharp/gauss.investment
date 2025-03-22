using FluentValidation;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Exceptions;

namespace Gauss.Investment.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMesssagesException.NAME_EMPTY);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMesssagesException.EMAIL_INVALID);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMesssagesException.EMAIL_EMPTY);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMesssagesException.PASSWORD_EMPTY);
        }
    }
}
