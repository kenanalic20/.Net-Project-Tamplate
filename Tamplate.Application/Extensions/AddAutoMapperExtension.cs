using Microsoft.Extensions.DependencyInjection;

namespace Tamplate.Application.Extensions
{
    public static class AddAutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
