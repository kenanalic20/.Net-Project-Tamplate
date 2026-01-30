using Tamplate.Application.Interfaces.Repositories;
using Tamplate.Domain.Data;
using Tamplate.Domain.Models;

namespace Tamplate.Infrastructure.Repositories
{
    public class ExampleRepository : BaseRepository<Example, int>, IExampleRepository
    {
        public ExampleRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Add any custom repository method implementations here if needed
    }
}
