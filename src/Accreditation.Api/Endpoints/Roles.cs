using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Accreditation.Api.Endpoints;

internal static class Roles
{
    //public const string StaffMember = "کاربر ستادی";
    ////public const string ArzyabMember = "کاربر ارزیاب کشوری";
    ////public const string OrgMember = "کاربر بیمارستان";
    ////public const string UniMember = "کاربر دانشگاهی";
    ////public const string ArzyabUniMember = "کاربر ارزیاب دانشگاهی";

    public const string StaffChief = "کاربر ستادی : رئیس مرکز نظارت و اعتباربخشی امور درمان";
    public const string StaffManager = "کاربر ستادی : مدیر گروه ارزشیابی و اعتباربخشی موسسات";
    public const string StaffAgent = "کاربر ستادی : کارشناس";
    public const string UniMember = "کاربر دانشگاهی";
    public const string ArzyabKeshvari = "کاربر ارزیاب کشوری";
    public const string ArzyabDaneshgahi = "کاربر ارزیاب دانشگاهی";
    public const string OrgChief = "کاربر موسسه : رئیس/مدیر اجرایی";
    public const string OrgAgent = "کاربر موسسه : ثبت اطلاعات";




}
