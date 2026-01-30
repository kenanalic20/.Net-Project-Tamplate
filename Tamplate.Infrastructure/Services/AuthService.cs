using Microsoft.AspNetCore.Identity;
using Tamplate.Application.Interfaces.Services;
using Tamplate.Application.DTOs;
using System.Security.Claims;
using AutoMapper;
using Tamplate.Application.Exceptions;
using Tamplate.Domain.Models;

namespace Tamplate.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IJwtService jwtService, 
            IMapper mapper
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<string> RegisterAsync(RegisterDto register)
        {

            var user = new User
            {
                UserName = register.Username,
                Email = register.Email,
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                string errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Bad Request";
                throw new TamplateValidationException(errorMessage);
            }

            string role = register.Platform.ToLower() == "desktop" ? "Admin" : "User";

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new Role { Name = role });


            await _userManager.AddToRoleAsync(user, role);

            var token = await _jwtService.GenerateJwtToken(user);

            return token;
        }

        public async Task<string> LoginAsync(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!result)
                throw new KeyNotFoundException("User credentials not valid");

            var token = await _jwtService.GenerateJwtToken(user);

            return token;
        }

        public async Task<string> GetUserIdAsync(ClaimsPrincipal user)
        {
            return await Task.FromResult(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<bool> IsAdminAsync(ClaimsPrincipal user)
        {
            if (user == null || !(user.Identity?.IsAuthenticated ?? false))
                throw new UnauthorizedAccessException("User not authenticated");

            return await Task.FromResult(user.IsInRole("Admin"));
        }

        public async Task<UserAuthDto> GetUserAsync(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new KeyNotFoundException("User not authenticated");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.UserName == null || user.Email == null)
                throw new KeyNotFoundException("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0)
                throw new KeyNotFoundException("User has no roles");
            
            var userDetails = _mapper.Map<UserAuthDto>(user);

            userDetails.Roles = roles;

            return userDetails;
        }
    }
}
