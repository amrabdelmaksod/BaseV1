using Hedaya.Application.MassCultures.Models;

namespace Hedaya.Application.MethodologicalExplanations.Models
{
    public class MethodlogicalExplanationResponse
    {
        public int TotalCount { get; set; }
        public List<SubCategoryDto> Categories { get; set; }
        public List<MethodlogicalExplanationDto> AllExplanations { get; set; }
    }
}
