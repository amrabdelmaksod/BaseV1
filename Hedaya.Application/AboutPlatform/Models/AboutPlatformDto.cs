namespace Hedaya.Application.AboutPlatform.Models
{
    public class AboutPlatformDto
    {
        public string VideoUrl { get; set; }
        public List<PlatformFeaturesDto> PlatformFeatures { get; set; }
        public List<PlatformFieldDto> PlatformFields { get; set; }
        public List<PlatformWorkAxesDto> PlatformWorkAxes { get; set; }
  
    }
}
