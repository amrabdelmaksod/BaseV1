namespace Hedaya.Application.Home.Models
{
    public class HomeQueryDto
    {
        public string IntroVideoUrl { get; set; }
        public List<OfferDto> Offers { get; set; }    
        public List<CategoryDto> Categories { get; set; }
        public List<MyCoursesDto> MyCourses { get; set; }
        public List<DiscoverCoursesDto> DiscoverCourses { get; set; }
        public List<MethodologicalExplanationDto> MethodologicalExplanations { get; set; }
        public List<BlogDto> Blogs { get; set; }



    }
}
