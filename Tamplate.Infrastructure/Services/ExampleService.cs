using AutoMapper;
using Tamplate.Application.DTOs;
using Tamplate.Application.Filters;
using Tamplate.Application.Interfaces.Repositories;
using Tamplate.Application.Interfaces.Services;
using Tamplate.Domain.Models;

namespace Tamplate.Infrastructure.Services
{
    public class ExampleService : BaseService<Example, ExampleDto, ExampleCreateDto, ExampleUpdateDto, ExampleQueryFilter, int>, IExampleService
    {
        public ExampleService(IExampleRepository repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }

        // Override ApplyFilter to implement custom filtering logic
        protected override IQueryable<Example> ApplyFilter(IQueryable<Example> query, ExampleQueryFilter? filter)
        {
            if (filter == null)
                return query;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(e => e.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(e => e.Description != null && e.Description.Contains(filter.Description));

            return query;
        }

        // You can override hook methods for custom logic
        // Example:
        // protected override async Task BeforeCreateAsync(Example entity, ExampleCreateDto dto)
        // {
        //     // Custom logic before creating
        //     await Task.CompletedTask;
        // }
    }
}
