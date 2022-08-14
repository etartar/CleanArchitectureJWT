using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Application.Common.Settings;
using CleanArchitectureJWT.Domain.Entities;

namespace CleanArchitectureJWT.Infrastructure.Persistence.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public RefreshTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
        {
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public string Generate(User user)
        {
            return _tokenGenerator.Generate(_jwtSettings.RefreshTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience, _jwtSettings.RefreshTokenExpirationMinutes);
        }
    }
}
