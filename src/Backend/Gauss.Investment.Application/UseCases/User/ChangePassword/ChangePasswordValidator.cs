using FluentValidation;
using Gauss.Investment.Application.SharedValidators;
using Gauss.Investment.Communication.Requests;

namespace Gauss.Investment.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePassword>());
        }
    }
}
