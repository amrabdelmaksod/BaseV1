namespace Hedaya.Application.TrainingPrograms.Models
{
    public class TrainingProgramDto
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsFav { get; set; }
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }

}
