namespace Hedaya.Application.MassCultures.Models
{
    public class MassCultureDetailsResponse
    {
        public MassCultureDetailsDto MassCulture { get; set; }
        public List<MassCultureDetailsDto> RelatedMassCultures { get; set; }
    }

}
