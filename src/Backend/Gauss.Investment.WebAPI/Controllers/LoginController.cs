

using Gauss.Investment.Application.UseCases.Login.DoLogin;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Gauss.Investment.WebAPI.Controllers
{
    public class LoginController : MainController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLogin request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
