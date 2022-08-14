using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using CleanArchitectureJWT.Domain.Entities;

namespace CleanArchitectureJWT.Application.Common.Interfaces
{
    public interface IAuthenticateService
    {
        Task<AuthenticateResponse> Authenticate(User user, CancellationToken cancellationToken);
    }
}
