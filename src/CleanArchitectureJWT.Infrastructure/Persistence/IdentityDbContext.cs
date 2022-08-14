using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Domain.Common;
using CleanArchitectureJWT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitectureJWT.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext, IApplicationDbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "API";
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "API";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "API";
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
