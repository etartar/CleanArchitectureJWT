using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using FluentValidation;

namespace CleanArchitectureJWT.Application.Common.Validators.Auth
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
