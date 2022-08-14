using CleanArchitectureJWT.Domain.Common;

namespace CleanArchitectureJWT.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
