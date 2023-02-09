using BaseV1.Domain.Entities;

namespace BaseV1.Domain
{
    public class TestClass
    {
        public int Id { get; set; }
        public required string Name { get; set; }
       
        public List<TestEntity> TestEntities { get; set; }
    }
}
