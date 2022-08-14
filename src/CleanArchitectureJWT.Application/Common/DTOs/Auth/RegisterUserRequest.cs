namespace CleanArchitectureJWT.Application.Common.DTOs.Auth
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
