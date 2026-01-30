using Microsoft.AspNetCore.Identity;
using Tamplate.Domain.Models;

namespace Tamplate.Application.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(User user);
    }
}
