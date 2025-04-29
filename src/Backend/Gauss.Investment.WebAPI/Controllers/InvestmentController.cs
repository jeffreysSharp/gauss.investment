using Gauss.Investment.Application.UseCases.User.ChangePassword;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Gauss.Investment.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Gauss.Investment.WebAPI.Controllers
{
    [AuthenticatedUser]
    public class InvestmentController : MainController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseResgiteredInvestment), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices IRegisterInvestmentUseCase useCase,
            [FromBody] RequestInvestment request)
        {
            var response = await ChangePasswordUseCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
