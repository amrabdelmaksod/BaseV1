namespace Hedaya.Application.MethodologicalExplanations.Models
{
    public class MethodlogicalExplanationDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Whatsapp { get; set; }
        public string Telegram { get; set; }
        public bool IsFav { get; set; }
        public string Duration { get; set; }
        public string InstructorName { get; set; }
        public string InstructorImgUrl { get; set; }
        public string InstructorDescription { get; set; }
        public string ImageUrl { get; set; }

        public int SubCategoryId { get; set; }
        public List<ExplanationVideoDto> ExplanationVideoDtos { get; set; }
        public List<ExplanationNoteDto> ExplanationNoteDtos { get; set; }
    }


}
