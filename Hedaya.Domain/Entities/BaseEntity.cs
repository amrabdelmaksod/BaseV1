namespace Hedaya.Domain.Entities
{
    public partial class BaseEntity
    {
        //public string CreatedById { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
