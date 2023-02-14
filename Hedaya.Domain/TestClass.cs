using Hedaya.Domain.Entities;

namespace Hedaya.Domain
{
    public class TestClass
    {
        public int Id { get; set; }
        public required string Name { get; set; }
       
        public List<TestEntity> TestEntities { get; set; }
    }
}
