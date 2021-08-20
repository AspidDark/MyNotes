using static MyNotes.Domain.Consts.DomainConsts;

namespace MyNotes.Contracts.V1.Request.Queries
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            PageNumber = 0;//to const?
            PageSize = PageElements.Mid;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize < PageElements.Max ? pageSize : PageElements.Max;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
