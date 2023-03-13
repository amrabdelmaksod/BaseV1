using System.ComponentModel.DataAnnotations;

namespace Hedaya.Domain.Enums
{
    public enum EducationalDegree
    {
        [Display(Name = "0 - لا يوجد", Description = "لا يوجد")]
        None = 0,

        [Display(Name = "1 - الثانوية العامة", Description = "الثانوية العامة")]
        HighSchool = 1,

        [Display(Name = "2 - الدبلوم الفني", Description = "الدبلوم الفني")]
        Diploma = 2,

        [Display(Name = "3 - البكالوريوس", Description = "البكالوريوس")]
        Bachelor = 3,

        [Display(Name = "4 - الماجستير", Description = "الماجستير")]
        Master = 4,

        [Display(Name = "5 - الدكتوراه", Description = "الدكتوراه")]
        PhD = 5
    }

}
