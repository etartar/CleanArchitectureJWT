using System.Security.Claims;

namespace CleanArchitectureJWT.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        string Generate(string secretKey, string issuer, string audience, double expires, IEnumerable<Claim> claims = null);
    }
}
