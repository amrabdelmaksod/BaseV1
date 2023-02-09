namespace BaseV1.Domain.Entities
{
    public class TestEntity
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int TestClassId { get; set; }
        public TestClass TestClass { get; set; }
    }
}
