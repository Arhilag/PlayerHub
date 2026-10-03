using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayerHub.Infrastructure.Persistence;

namespace PlayerHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Connection string 'Default' is not configured.");

            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));

            return services;
        }
    }
}
