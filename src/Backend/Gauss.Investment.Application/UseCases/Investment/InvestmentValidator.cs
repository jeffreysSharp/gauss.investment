using FluentValidation;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Exceptions;

namespace Gauss.Investment.Application.UseCases.Investment
{
    public class InvestmentValidator : AbstractValidator<RequestInvestment>
    {
        public InvestmentValidator()
        {
            RuleFor(invest => invest.Title).NotEmpty().WithMessage(ResourceMesssagesException.INVESTMENT_TITLE_EMPTY);
            RuleFor(invest => invest.InvestmentCategory).IsInEnum().WithMessage(ResourceMesssagesException.INVESTMENT_CATEGORY_NOT_SUPPORTED);
            RuleFor(invest => invest.InvestmentType).IsInEnum().WithMessage(ResourceMesssagesException.INVESTMENT_TYPE_NOT_SUPPORTED);
            RuleFor(invest => invest.InvestmentIssuers.Count).GreaterThan(0).WithMessage(ResourceMesssagesException.INVESTMENT_USSUER_NOT_SUPPORTED);
        }
    }
}
