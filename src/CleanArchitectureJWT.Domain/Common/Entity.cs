using CleanArchitectureJWT.Domain.Interfaces;

namespace CleanArchitectureJWT.Domain.Common
{
    public abstract class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        protected Entity() { }
        protected Entity(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return false;

            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity a, Entity b) => a.Equals(b);
        public static bool operator !=(Entity a, Entity b) => !(a == b);
    }
}
