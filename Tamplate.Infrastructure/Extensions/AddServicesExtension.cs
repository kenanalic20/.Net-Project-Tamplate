using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tamplate.Application.Interfaces.Repositories;
using Tamplate.Application.Interfaces.Services;
using Tamplate.Infrastructure.Repositories;
using Tamplate.Infrastructure.Services;
using QuestPDF.Infrastructure;

namespace Tamplate.Infrastructure.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var rabbitMqHost = Environment.GetEnvironmentVariable("RabbitMQ__ConnectionString")??"localhost";
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddSingleton<IRabbitMqPublisher>(sp => new RabbitMqPublisherService(rabbitMqHost));
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped(typeof(IService<,,,,,>), typeof(BaseService<,,,,,>));
            services.AddTransient<IFileService, FileService>();
            

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IPaymentService, StripePaymentService>();



            return services;
        }
    }
}
