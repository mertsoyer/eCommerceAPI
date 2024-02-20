using eCommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using eCommerceAPI.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            var result = await _mediator.Send(createUserCommandRequest);
            return Ok(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
    }
}
