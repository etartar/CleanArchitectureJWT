namespace CleanArchitectureJWT.Application.Common.DTOs.Auth
{
    public class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NormalizedEmail => Email.ToUpper();
    }
}
