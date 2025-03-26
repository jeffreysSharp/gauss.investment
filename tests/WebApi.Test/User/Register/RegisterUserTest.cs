using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Exceptions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : GaussInvestmentClassFixture
    {
        private readonly string _method = "user";

        public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserBuilder.Build();

            var response = await DoPost(_method, request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
            responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().Should().NotBeNullOrEmpty();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestRegisterUserBuilder.Build();
            request.Name = string.Empty;

            var response = await DoPost(_method, request, culture);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expetedMessage = ResourceMesssagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expetedMessage));
        }
    }
}
