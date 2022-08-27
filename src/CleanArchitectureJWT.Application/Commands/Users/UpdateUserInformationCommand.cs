using CleanArchitectureJWT.Application.Common.DTOs.Users;
using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Models;
using CleanArchitectureJWT.Application.Common.Wrappers;
using CleanArchitectureJWT.Domain.Exceptions;
using Forbids;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureJWT.Application.Commands.Users
{
    public record UpdateUserInformationCommand(UpdateUserInformationRequest UpdateUserInformationRequest) : IRequestWrapper<bool>;

    public class UpdateUserInformationCommandHandler : IHandlerWrapper<UpdateUserInformationCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IForbid _forbid;

        public UpdateUserInformationCommandHandler(IApplicationDbContext context, IMapper mapper, IForbid forbid)
        {
            _context = context;
            _mapper = mapper;
            _forbid = forbid;
        }

        public async Task<IResponse<bool>> Handle(UpdateUserInformationCommand request, CancellationToken cancellationToken)
        {
            var updateInfoDto = request.UpdateUserInformationRequest;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == updateInfoDto.NormalizedEmail, cancellationToken);
            _forbid.Null(user, new UserNotFoundException());
            _mapper.Map(updateInfoDto, user);
            var updateResult = _context.Users.Update(user);
            int result = await _context.SaveChangesAsync(cancellationToken);
            return new Response<bool>(result > 0, result > 0);
        }
    }
}
