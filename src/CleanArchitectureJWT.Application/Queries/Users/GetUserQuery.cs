using CleanArchitectureJWT.Application.Common.DTOs.Users;
using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Entities;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitectureJWT.Application.Queries.Users
{
    public record GetUserQuery(Expression<Func<User, bool>> Predicate) : IRequestWrapper<GetUserRequest>;

    public class GetUserQueryHandler : IHandlerWrapper<GetUserQuery, GetUserRequest>
    {
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public GetUserQueryHandler(IApplicationDbContext context, IForbid forbid)
        {
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<GetUserRequest>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(request.Predicate, cancellationToken);
            _forbid.Null(user, new UserNotFoundException());
            return Response.Success(user.Adapt<GetUserRequest>());
        }
    }
}
