using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureJWT.Application.Commands.Auth
{
    public record RefreshCommand(RefreshRequest RefreshRequest) : IRequestWrapper<AuthenticateResponse>;

    public class RefreshCommandHandler : IHandlerWrapper<RefreshCommand, AuthenticateResponse>
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public RefreshCommandHandler(IAuthenticateService authenticateService, IRefreshTokenValidator refreshTokenValidator, IApplicationDbContext context, IForbid forbid)
        {
            _authenticateService = authenticateService;
            _refreshTokenValidator = refreshTokenValidator;
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<AuthenticateResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var refreshRequest = request.RefreshRequest;
            var isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            _forbid.False(isValidRefreshToken, new InvalidRefreshTokenException());
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshRequest.RefreshToken, cancellationToken);
            _forbid.Null(refreshToken, new InvalidRefreshTokenException());
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == refreshToken.UserId, cancellationToken);
            _forbid.Null(user, new UserNotFoundException());
            var authenticateResult = await _authenticateService.Authenticate(user, cancellationToken);
            return Response.Success(authenticateResult);
        }
    }
}
