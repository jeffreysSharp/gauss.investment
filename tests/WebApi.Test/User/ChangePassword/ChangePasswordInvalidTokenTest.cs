using CommonTestUtilities.Tokens;
using FluentAssertions;
using Gauss.Investment.Communication.Requests;

namespace WebApi.Test.User.ChangePassword
{
    public class ChangePasswordInvalidTokenTest : GaussInvestmentClassFixture
    {
        private const string METHOD = "user/change-password";
        public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = new RequestChangePassword();

            var response = await DoPut(METHOD, request, token: "tokenInvalid");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = new RequestChangePassword();

            var response = await DoPut(METHOD, request, token: string.Empty);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

            var request = new RequestChangePassword();

            var response = await DoPut(METHOD, request, token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
