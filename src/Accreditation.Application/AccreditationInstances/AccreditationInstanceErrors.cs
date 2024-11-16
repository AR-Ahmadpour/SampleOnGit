using SharedKernel;

namespace Accreditation.Application.AccreditationInstances;
public static class AccreditationInstanceErrors
{
    public static Error NotFound => Error.NotFound("AccreditationInstance.NotFound", "ارزیابی پایه مورد نظر یافت نشد");
    public static Error IsLockedDelete => Error.NotFound("AccreditationInstance.IsLockedDelete", "ارزیابی مورد نظر امکان حذف ندارد");
    public static Error IsLockedMasterDelete => Error.NotFound("AccreditationInstance.IsLockedMasterDelete", " .ابتدا آن را حذف نمایید.این ارزیابی پایه ارزیابی دیگری است.");
    public static Error FromToDateRequiered => Error.NotFound("AccreditationInstance.FromToDateRequiered", "تاریخ شروع وپایان برای این نوع ارزیابی الزامی است");
    public static Error ArzyabiIsLocked => Error.NotFound("AccreditationInstance.ArzyabiIsLocked", "در این مرحله از ارزیابی امکان تغییر ارزیاب ها وجود ندارد ");
    public static Error ConflictTypeAccreditation => Error.Conflict("AccreditationInstance.ConflictTypeAccreditation", "ارزیابی داخلی بدون ارزیاب باشد");
    public static Error AccreditationSarparast => Error.Conflict("AccreditationInstance.AccreditationSarparast", "برای ارزیابی باید ارزیاب سرپرست معیین شود");
    public static Error AccreditationPayeh => Error.Conflict("AccreditationInstance.AccreditationPayeh", "برای ارزیابی باید ارزیابی پایه معیین شود");
    public static Error ExceptionInsertAccreditation => Error.Conflict("AccreditationInstance.ExceptionInsertAccreditation", "ثبت ارزیابی داخلی با خطا شد");

}
