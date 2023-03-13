namespace Hedaya.Application.Infrastructure
{
    public interface IPagedQuery<T>
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
