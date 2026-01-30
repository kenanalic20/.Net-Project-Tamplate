
using Tamplate.Application.Common;

namespace Tamplate.Application.Filters
{
    public class CattleQueryFilter : PaginationRequest
    {
        public string? Search { get; set; }
        public int? CattleCategoryId { get; set; }
        public string? OrderBy { get; set; }

    }
}
