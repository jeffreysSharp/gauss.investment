using Gauss.Investment.Application.UseCases.User.Profile;
using Gauss.Investment.Application.UseCases.User.Register;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Gauss.Investment.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Gauss.Investment.WebAPI.Controllers
{
    public class UserController : MainController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices]IRegisterUseCase useCase,
            [FromBody]RequestRegisterUser request)
        {

            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfile), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }
    }
}
