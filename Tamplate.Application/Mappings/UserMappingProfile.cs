using AutoMapper;
using Tamplate.Application.DTOs;
using Tamplate.Domain.Models;

namespace Tamplate.Application.Mappings
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile() 
        { 
            CreateMap<User, UserDto>();
            CreateMap<User, UserAuthDto>();
        }
    }
}
