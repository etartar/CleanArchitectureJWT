using CleanArchitectureJWT.Application.Common.DTOs.Auth;
using FluentValidation;

namespace CleanArchitectureJWT.Application.Common.Validators.Users
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().Matches(x => x.Password);
        }
    }
}
