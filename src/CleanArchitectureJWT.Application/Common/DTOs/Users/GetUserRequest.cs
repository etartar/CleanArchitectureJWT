namespace CleanArchitectureJWT.Application.Common.DTOs.Users
{
    public class GetUserRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
