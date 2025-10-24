using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost] /* Defino o tipo de requisção */
        [ProducesResponseType(typeof(ResponseResgisteredUserJson),StatusCodes.Status201Created)] /* Define o tipo de resposta e o código HTTP */
        public IActionResult Register(RequestRegisterUserJson request)
        {
            var useCase = new RegisterUserUseCase();

            var result = useCase.Execute(request);

            return Created(string.Empty, result); /* Retorna o código HTTP 201 Created */
        }
    }
}
