using CleanArchitectureJWT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureJWT.Infrastructure.Persistence.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Token).IsRequired();

            builder.HasOne(x => x.User).WithMany(y => y.RefreshTokens).HasForeignKey(x => x.UserId);

            builder.HasQueryFilter(x => !x.Deleted.HasValue);
        }
    }
}
