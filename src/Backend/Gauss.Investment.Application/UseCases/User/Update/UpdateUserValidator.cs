using FluentValidation;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Domain.Extensions;
using Gauss.Investment.Exceptions;

namespace Gauss.Investment.Application.UseCases.User.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMesssagesException.NAME_EMPTY);
            RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMesssagesException.EMAIL_EMPTY);

            When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () =>
            {
                RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMesssagesException.EMAIL_INVALID);
            });
        }
    }
}
