using CleanArchitectureJWT.Application.Commands.Auth;
using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureJWT.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _bus;

        public AuthController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequest loginUserRequest, CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new LoginUserCommand(loginUserRequest), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new RegisterUserCommand(registerUserRequest), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshRequest refreshRequest, CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new RefreshCommand(refreshRequest), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new LogOutCommand(), cancellationToken));
        }
    }
}
