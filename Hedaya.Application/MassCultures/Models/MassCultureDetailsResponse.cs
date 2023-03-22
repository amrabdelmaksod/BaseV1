namespace Hedaya.Application.MassCultures.Models
{
    public class MassCultureDetailsResponse
    {
        public MassCultureDto MassCulture { get; set; }
        public List<MassCultureDetailsDto> RelatedMassCultures { get; set; }
    }

}
