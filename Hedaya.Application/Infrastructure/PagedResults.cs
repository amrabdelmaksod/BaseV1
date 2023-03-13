namespace Hedaya.Application.Infrastructure
{
    public class PagedResults<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
