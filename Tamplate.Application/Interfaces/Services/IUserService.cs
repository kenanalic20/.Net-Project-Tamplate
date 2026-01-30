using Tamplate.Application.Common;
using Tamplate.Application.DTOs;
using Tamplate.Application.Filters;

namespace Tamplate.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryFilter? filter);
        Task<UserDto?> GetByIdAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> UpdateUserCredentialsAsync(string userId,UserAdminDto dto);
        Task<bool> UpdatePhoneNumberAsync(string userId, string phoneNumber);
        Task<bool> UpdateEmailAsync(string userId, string email);
    }
}
