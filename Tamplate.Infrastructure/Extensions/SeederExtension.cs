using Microsoft.Extensions.DependencyInjection;
using Tamplate.Infrastructure.Seeders;

namespace Tamplate.Infrastructure.Extensions
{
    public static class SeederExtension
    {
        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<RoleSeeder>();
            services.AddScoped<IdentitySeeder>();
            return services;
        }
    }
}
