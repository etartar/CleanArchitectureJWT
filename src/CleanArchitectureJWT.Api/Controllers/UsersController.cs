using CleanArchitectureJWT.Application.Commands.Users;
using CleanArchitectureJWT.Application.Common.DTOs.Users;
using CleanArchitectureJWT.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureJWT.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _bus;

        public UsersController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new GetAllUserQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new GetUserQuery(x => x.Id == id), cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserInformation(UpdateUserInformationRequest updateUserInformationRequest, CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new UpdateUserInformationCommand(updateUserInformationRequest), cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = new())
        {
            return Ok(await _bus.Send(new DeleteUserCommand(id), cancellationToken));
        }
    }
}
