using CleanArchitectureJWT.Application.Common.DTOs.Users;
using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureJWT.Application.Queries.Users
{
    public record GetAllUserQuery : IRequestWrapper<IReadOnlyList<GetUserRequest>>;

    public class GetAllUserQueryHandler : IHandlerWrapper<GetAllUserQuery, IReadOnlyList<GetUserRequest>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public GetAllUserQueryHandler(IApplicationDbContext context, IForbid forbid)
        {
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<IReadOnlyList<GetUserRequest>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ProjectToType<GetUserRequest>().ToListAsync(cancellationToken);
            _forbid.NullOrEmpty(users, new UserNotFoundException());
            return Response.Success<IReadOnlyList<GetUserRequest>>(users);
        }
    }
}
