using CleanArchitectureJWT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureJWT.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
