﻿using System.ComponentModel;

namespace BaseV1.Domain.Enums
{
    public enum UserType
    {
        [Description("مدير النظام")]
        Admin =1,
        [Description("مستخدم")]
        User = 2,
   
    }
}
