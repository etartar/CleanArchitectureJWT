namespace CleanArchitectureJWT.Domain.Common
{
    public class AuditableEntity<T> : Entity<T>
    {
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}
