using SharedKernel;

namespace Accreditation.Application.EtebarDorehs;

public static class EtebarDorehErrors
{
    public static Error TitleNotUnique => Error.Conflict("EtebarDorehs.TitleNotUnique", "عنوان تکراری است");

    public static Error ActivatingTwoPeriodIsInvalid => Error.Conflict("EtebarDorehs.SameTime", "امکان فعال کردن دو دوره به طور همزمان وجود ندارد");

    public static Error NotFound => Error.NotFound("EtebarDorehs.NotFound", "اعتبار بخشی مورد نظر یافت نشد");
    public static Error NotActive => Error.NotFound("EtebarDorehs.NotActive", "اعتبار بخشی موردغیر فعال شده");

    //public static Error TitleIsRequired => Error.Failure("EtebarDorehs.TitleIsRequired", "عنوان الزامی است");

    //public static Error MaximumLengthIsNotValid => Error.Failure("EtebarDorehs.MaximumLengthIsNotValid", "حداکثر طول رشته مجاز 1000 کاراکتر می باشد");

    //public static Error StartDateIsRequired => Error.Failure("EtebarDorehs.StartDateIsRequired", "تاریخ آغاز دوره الزامی است");

    public static Error StartDateHasToBeLessThanEndDate => Error.Failure("EtebarDorehs.StartDateHasToBeLessThanEndDate", "تاریخ آغاز دوره نباید از تاریخ پایان بزرگتر باشد");

    public static Error EndDateIsRequired => Error.Failure("EtebarDorehs.EndDateIsRequired", "تاریخ آغاز دوره الزامی است");

    //public static Error EndDateHasToBeGreaterThanStartDate => Error.Failure("EtebarDorehs.EndDateHasToBeGreaterThanStartDate", "تاریخ آغاز دوره نباید از تاریخ پایان بزرگتر باشد");

    //public static Error SortOrderHasToBeGreaterThanZero => Error.Failure("EtebarDorehs.SortOrderHasToBeGreaterThanZero", "ترتیب پیمایش باید بزرگتر از صفر باشد");

    //public static Error OrgTypeIsRequired => Error.Failure("EtebarDorehs.OrgTypeIsRequired", "نوع سازمان الزامی است");
    public static Error CurrentEtebrDorehCanNotRemoved => Error.Failure("EtebarDorehs.CurrentEtebrDorehCanNotRemoved", "اعتبار دوره جاری قابل حذف نیست");

}
