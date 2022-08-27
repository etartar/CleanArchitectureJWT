using CleanArchitectureJWT.Domain.Interfaces;

namespace CleanArchitectureJWT.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }

        protected Entity() { }
        protected Entity(T id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<T> other)
                return false;

            if (ReferenceEquals(this, other))
                return false;

            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b) => a.Equals(b);
        public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);
    }
}
