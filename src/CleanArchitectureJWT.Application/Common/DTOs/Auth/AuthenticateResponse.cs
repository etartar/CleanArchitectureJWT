namespace CleanArchitectureJWT.Application.Common.DTOs.Auth
{
    public class AuthenticateResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
