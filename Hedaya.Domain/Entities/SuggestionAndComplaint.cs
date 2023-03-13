namespace Hedaya.Domain.Entities
{
    public class SuggestionAndComplaint
    {
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
