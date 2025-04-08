using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Application.UseCases.Investment;
using Gauss.Investment.Exceptions;

namespace Validators.Test.Investment
{
    public class InvestmentValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new InvestmentValidator();

            var request = RequestInvestmentBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Invalid_InvestmentCategory()
        {
            var validator = new InvestmentValidator();

            var request = RequestInvestmentBuilder.Build();
            request.InvestmentCategory = (Gauss.Investment.Communication.Enums.InvestmentCategory?)1000;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMesssagesException.INVESTMENT_CATEGORY_NOT_SUPPORTED));
        }

        [Fact]
        public void Error_Invalid_InvestmentType()
        {
            var validator = new InvestmentValidator();

            var request = RequestInvestmentBuilder.Build();
            request.InvestmentType = (Gauss.Investment.Communication.Enums.InvestmentType?)1000;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMesssagesException.INVESTMENT_TYPE_NOT_SUPPORTED));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("           ")]
        [InlineData("")]
        public void Error_Empty_Tytle(string title)
        {
            var validator = new InvestmentValidator();

            var request = RequestInvestmentBuilder.Build();
            request.Title = title;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMesssagesException.INVESTMENT_TITLE_EMPTY));
        }

        [Fact]
        public void Success_InvestmentCategory_Null()
        {
            var validator = new InvestmentValidator();

            var request = RequestInvestmentBuilder.Build();
            request.InvestmentCategory = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Success_InvestmentType_Null()
        {
            var validator = new InvestmentValidator();

            var request = RequestInvestmentBuilder.Build();
            request.InvestmentType = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

    }
}
