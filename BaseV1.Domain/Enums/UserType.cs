using System.ComponentModel;

namespace BaseV1.Domain.Enums
{
    public enum UserType
    {
        [Description("مدير النظام")]
        Admin =1,
        [Description("مدرب")]
        Instructor = 2,
        [Description("متدرب")]
        Trainee = 3,
    }
}
