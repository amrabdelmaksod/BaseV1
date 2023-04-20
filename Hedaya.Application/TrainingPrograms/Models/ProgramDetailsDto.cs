namespace Hedaya.Application.TrainingPrograms.Models
{
    public class ProgramDetailsDto
    {
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFav { get; set; }
        public bool IsEnrolled { get; set; }
        public List<TrainingProgramNoteDto> TrainingProgramNotes { get; set; }
        public List<CoursesLiDto> Courses { get; set; }
        public List<TrainingProgramDto> RelatedPrograms { get; set; }
    }

}
