using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Settings;
using CleanArchitectureJWT.Domain.Entities;
using System.Security.Claims;

namespace CleanArchitectureJWT.Infrastructure.Persistence.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
        {
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public string Generate(User user)
        {
            List<Claim> claims = new()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            return _tokenGenerator.Generate(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience, _jwtSettings.AccessTokenExpirationMinutes, claims);
        }
    }
}
