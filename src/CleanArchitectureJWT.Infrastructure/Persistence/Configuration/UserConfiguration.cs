using CleanArchitectureJWT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureJWT.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.NormalizedName).IsRequired();
            builder.Property(p => p.Surname).IsRequired();
            builder.Property(p => p.NormalizedSurname).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.NormalizedEmail).IsRequired();
            builder.Property(p => p.EmailConfirmed).HasDefaultValue(false);
            builder.Property(p => p.PhoneNumberConfirmed).HasDefaultValue(false);
            builder.Property(p => p.LockoutEnabled).HasDefaultValue(false);
            builder.Property(p => p.AccessFailedCount).HasDefaultValue(0);

            builder.HasMany(x => x.RefreshTokens).WithOne(y => y.User);

            builder.HasQueryFilter(x => !x.Deleted.HasValue);
        }
    }
}
