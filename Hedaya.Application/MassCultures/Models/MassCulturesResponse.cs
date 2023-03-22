namespace Hedaya.Application.MassCultures.Models
{
    public class MassCulturesResponse
    {
        public int TotalCount { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<MassCultureDto> AllCultures { get; set; }
    }
}
