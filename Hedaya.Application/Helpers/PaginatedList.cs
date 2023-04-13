namespace Hedaya.Application.Helpers
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int totalCount, int pageNumber, int pageSize, int totalPages)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }

}
