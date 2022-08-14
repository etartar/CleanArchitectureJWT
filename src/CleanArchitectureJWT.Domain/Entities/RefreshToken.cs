using CleanArchitectureJWT.Domain.Common;

namespace CleanArchitectureJWT.Domain.Entities
{
    public class RefreshToken : AuditableEntity
    {
        public RefreshToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
