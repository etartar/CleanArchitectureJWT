using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureJWT.Application.Commands.Auth
{
    public record LogOutCommand : IRequestWrapper<Unit>;

    public class LogOutCommandHandler : IHandlerWrapper<LogOutCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public LogOutCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext context, IForbid forbid)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<Unit>> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            var getUserId = _httpContextAccessor.HttpContext?.User.FindFirst("id");
            _forbid.Null(getUserId, new UserNotFoundException());
            var userId = Guid.Parse(getUserId.Value);
            _forbid.NullOrEmpty(userId, new UserNotFoundException());
            var refreshTokens = await _context.RefreshTokens.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
            _context.RefreshTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync(cancellationToken);
            return Response.Success(Unit.Value);
        }
    }
}
