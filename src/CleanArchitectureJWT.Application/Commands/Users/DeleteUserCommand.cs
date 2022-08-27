using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureJWT.Application.Commands.Users
{
    public record DeleteUserCommand(Guid Id) : IRequestWrapper<bool>;

    public class DeleteUserCommandHandler : IHandlerWrapper<DeleteUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public DeleteUserCommandHandler(IApplicationDbContext context, IForbid forbid)
        {
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            _forbid.Null(user, new UserNotFoundException());
            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return new Response<bool>(result > 0, result > 0);
        }
    }
}
