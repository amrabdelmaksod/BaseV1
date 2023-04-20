namespace Hedaya.Domain.Entities
{
    public class TrainingProgramNote
    {
        public int Id { get; set; }
        public string TextAr{ get; set; }
        public string? TextEn { get; set; }
        public byte SortIndex { get; set; }
        public int TrainingProgramId { get; set; }


        public virtual TrainingProgram TrainingProgram { get; set; }
    }
}
