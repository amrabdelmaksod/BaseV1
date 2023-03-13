namespace Hedaya.WebApi.Models
{
    public class UserParamsVM
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
    }
}
