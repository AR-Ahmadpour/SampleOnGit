using System.ComponentModel.DataAnnotations;

namespace Accrediation.Domain.Identity.Enums
{
    public enum Gender
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2,

        [Display(Name = "تعیین نشده")]
        NotDetermined = 3
    }
}
