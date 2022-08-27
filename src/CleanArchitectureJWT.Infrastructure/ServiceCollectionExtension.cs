using CleanArchitectureJWT.Application.Common.Interfaces;
using CleanArchitectureJWT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureJWT.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName);
                        builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            });

            services.AddScoped<IApplicationDbContext>(x => x.GetService<IdentityDbContext>()!);
        }
    }
}
