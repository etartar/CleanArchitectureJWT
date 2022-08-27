using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Entities;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitectureJWT.Application.Commands.Auth
{
    public record LoginUserCommand(LoginUserRequest LoginUserRequest) : IRequestWrapper<AuthenticateResponse>;

    public class LoginUserCommandHandler : IHandlerWrapper<LoginUserCommand, AuthenticateResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuthenticateService _authenticateService;
        private readonly IForbid _forbid;

        public LoginUserCommandHandler(IApplicationDbContext context, IForbid forbid)
        {
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<AuthenticateResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(Predicate(request.LoginUserRequest), cancellationToken);
            _forbid.Null(user, new SignInException());
            var authenticateResult = await _authenticateService.Authenticate(user, cancellationToken);
            return Response.Success(authenticateResult);
        }

        private Expression<Func<User, bool>> Predicate(LoginUserRequest request)
        {
            return x => x.NormalizedEmail == request.NormalizedEmail && x.Password == request.Password;
        }
    }
}
