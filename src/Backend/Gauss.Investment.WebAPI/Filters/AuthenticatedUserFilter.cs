using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Exceptions.ExceptionsBase;
using Gauss.Investment.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Gauss.Investment.Domain.Extensions;
using Gauss.Investment.Communication.Responses;

namespace Gauss.Investment.WebAPI.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;

        public AuthenticatedUserFilter(
            IAccessTokenValidator accessTokenValidator,
            IUserReadOnlyRepository repository)
        {
            _accessTokenValidator = accessTokenValidator;
            _repository = repository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);
                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
                var exist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);

                if (exist.IsFalse())
                {
                    throw new GaussInvestmentException(ResourceMesssagesException.USER_WITHOUT_PERMISSION_ACCES_RESOURCE);
                }
            }
            catch (GaussInvestmentException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseError(ex.Message));
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseError("TokenIsExpired")
                {
                    TokenIsExpired = true
                });
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseError(ResourceMesssagesException.USER_WITHOUT_PERMISSION_ACCES_RESOURCE));
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authentication))
            {
                throw new GaussInvestmentException(ResourceMesssagesException.NO_TOKEN);
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}

