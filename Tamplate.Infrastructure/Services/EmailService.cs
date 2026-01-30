
using Tamplate.Application.Interfaces.Repositories;
using Tamplate.Application.Interfaces.Services;
using Tamplate.Messages;

namespace Tamplate.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        public EmailService
        (
            IRabbitMqPublisher rabbitMqPublisher 
        )
        {
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public virtual async Task SendEmailAsync(string userId, EmailMessage message, bool skipSettingsCheck = false)
        {
            await _rabbitMqPublisher.PublishAsync(message);
        }

    }
}
