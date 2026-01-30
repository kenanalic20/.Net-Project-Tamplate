using Tamplate.Messages;

namespace Tamplate.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string userId, EmailMessage message, bool skipSettingsCheck = false);
    }
}
