using Tamplate.Application.Common;

namespace Tamplate.Application.Filters
{
    public class OrderItemsQueryFilter : PaginationRequest
    {
        public int OrderId { get; set; }
    }
}
