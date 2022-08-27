using CleanArchitectureJWT.Domain.Common;

namespace CleanArchitectureJWT.Domain.Entities
{
    public class RefreshToken : AuditableEntity<Guid>
    {
        public RefreshToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        #region Properties

        public Guid UserId { get; set; }
        public string Token { get; set; }

        #endregion

        #region Virtuals

        public virtual User User { get; set; }

        #endregion
    }
}
