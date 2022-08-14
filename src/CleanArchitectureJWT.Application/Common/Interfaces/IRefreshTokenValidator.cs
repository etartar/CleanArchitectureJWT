namespace CleanArchitectureJWT.Application.Common.Interfaces
{
    public interface IRefreshTokenValidator
    {
        bool Validate(string refreshToken);
    }
}
