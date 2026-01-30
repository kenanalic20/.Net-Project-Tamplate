using AutoMapper;
using Tamplate.Application.DTOs;
using Tamplate.Domain.Models;

namespace Tamplate.Application.Mappings
{
    public class ExampleMappingProfile : Profile
    {
        public ExampleMappingProfile()
        {
            CreateMap<Example, ExampleDto>();
            CreateMap<ExampleCreateDto, Example>();
            CreateMap<ExampleUpdateDto, Example>();
        }
    }
}
