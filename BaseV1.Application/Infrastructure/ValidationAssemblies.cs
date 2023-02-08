using System.Reflection;

namespace BaseV1.Application.Infrastructure
{
    public class ValidationAssemblies
    {
        public IEnumerable<Assembly> assemblies { get; set; }
        public ValidationAssemblies()
        {
            assemblies = new List<Assembly>
            {
                //typeof( CreateDepartmentCommandValidator).GetTypeInfo().Assembly,
                //typeof( UpdateDepartmentCommandValidator).GetTypeInfo().Assembly
            };
        }
    }
}
