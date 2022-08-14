using CleanArchitectureJWT.Domain.Entities;

namespace CleanArchitectureJWT.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
