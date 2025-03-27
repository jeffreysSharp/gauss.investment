using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using Gauss.Investment.Exceptions;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Update
{
    public class UpdateUserTest : GaussInvestmentClassFixture
    {
        private const string METHOD = "user";

        private readonly Guid _userIdentifier;

        public UpdateUserTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestUpdateUserBuilder.Build();

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut(METHOD, request, token);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestUpdateUserBuilder.Build();
            request.Name = string.Empty;

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoPut(METHOD, request, token, culture);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMesssagesException.ResourceManager.GetString("NAME_EMPTY", new System.Globalization.CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
