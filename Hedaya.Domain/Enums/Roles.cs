using System.ComponentModel;

namespace Hedaya.Domain.Enums
{
    public enum Roles
    {
        [Description("مدير النظام")]
        SuperAdmin = 1,
        
        [Description("خدمة العملاء")]
        Support = 2,

        [Description("مستخدم")]
        User = 2,


    }
}
