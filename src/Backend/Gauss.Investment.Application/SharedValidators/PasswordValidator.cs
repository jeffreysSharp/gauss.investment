using FluentValidation;
using FluentValidation.Validators;
using Gauss.Investment.Exceptions;

namespace Gauss.Investment.Application.SharedValidators
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {
        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMesssagesException.PASSWORD_EMPTY);
                return false;
            }

            if (password.Length < 6)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMesssagesException.INVALID_PASSWORD);
                return false;
            }

            return true;
        }

        public override string Name => "PasswordValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";

    }
}
