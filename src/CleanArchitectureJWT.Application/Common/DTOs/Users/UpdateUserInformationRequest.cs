namespace CleanArchitectureJWT.Application.Common.DTOs.Users
{
    public class UpdateUserInformationRequest
    {
        public string Name { get; set; }
        public string NormalizedName => Name.ToUpper();
        public string Surname { get; set; }
        public string NormalizedSurname => Surname.ToUpper();
        public string Email { get; set; }
        public string NormalizedEmail => Email.ToUpper();
        public string? PhoneNumber { get; set; }
    }
}
