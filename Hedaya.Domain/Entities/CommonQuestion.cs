namespace Hedaya.Domain.Entities
{
    public class CommonQuestion : BaseEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Response { get; set; }
    }
}
