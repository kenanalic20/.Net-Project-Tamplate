using Tamplate.Application.DTOs;
using Tamplate.Application.Filters;
using Tamplate.Domain.Models;

namespace Tamplate.Application.Interfaces.Services
{
    public interface IExampleService : IService<Example, ExampleDto, ExampleCreateDto, ExampleUpdateDto, ExampleQueryFilter, int>
    {
        // Add any custom service methods here if needed
    }
}
