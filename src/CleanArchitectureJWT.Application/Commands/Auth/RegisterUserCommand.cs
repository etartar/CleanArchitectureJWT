using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Entities;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using MediatR;

namespace CleanArchitectureJWT.Application.Commands.Auth
{
    public record RegisterUserCommand(RegisterUserRequest RegisterUserRequest) : IRequestWrapper<Unit>;

    public class RegisterUserCommandHandler : IHandlerWrapper<RegisterUserCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public RegisterUserCommandHandler(IApplicationDbContext context, IForbid forbid)
        {
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<Unit>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User().CreateUser(request.RegisterUserRequest.Name, 
                                            request.RegisterUserRequest.Surname, 
                                            request.RegisterUserRequest.Email, 
                                            request.RegisterUserRequest.EmailConfirmed, 
                                            request.RegisterUserRequest.PhoneNumber, 
                                            request.RegisterUserRequest.PhoneNumberConfirmed);

            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync(cancellationToken);
            _forbid.False(result <= 0, new RegisterException());
            return Response.Success(Unit.Value);
        }
    }
}
