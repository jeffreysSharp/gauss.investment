using Gauss.Investment.Application.UseCases.User.Register;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gauss.Investment.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices]IRegisterUseCase useCase,
            [FromBody]RequestRegisterUserJson request)
        {

            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
