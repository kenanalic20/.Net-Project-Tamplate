using Tamplate.Application.Common;

namespace Tamplate.Application.Filters
{
    public class UserQueryFilter:PaginationRequest
    {
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
    }
}
