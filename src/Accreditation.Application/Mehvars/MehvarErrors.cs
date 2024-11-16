

using SharedKernel;

namespace Accreditation.Application.Mehvars
{
    public static class MehvarErrors
    {
        public static Error TitleNotUnique => Error.Conflict("Mehvars.TitleNotUnique", "عنوان تکراری است");

        public static Error ActivatingTwoPeriodIsInvalid => Error.Conflict("Mehvars.TitleNotUnique", "امکان فعال کردن دو دوره به طور همزمان وجود ندارد");

        public static Error NotFound => Error.NotFound("Mehvars.NotFound", "محور مورد نظر یافت نشد");
        public static Error NotActive => Error.NotFound("Mehvars.NotActive", "محور مورد نظر غیر فعال شده");

        public static Error TitleIsRequired => Error.Failure("Mehvars.TitleIsRequired", "عنوان الزامی است");

        public static Error MaximumLengthIsNotValid => Error.Failure("Mehvars.MaximumLengthIsNotValid", "حداکثر طول رشته مجاز 1000 کاراکتر می باشد");

        public static Error StartDateIsRequired => Error.Failure("Mehvars.StartDateIsRequired", "تاریخ آغاز دوره الزامی است");

        public static Error StartDateHasToBeLessThanEndDate => Error.Failure("Mehvars.StartDateHasToBeLessThanEndDate", "تاریخ آغاز دوره نباید از تاریخ پایان بزرگتر باشد");

        public static Error EndDateIsRequired => Error.Failure("Mehvars.EndDateIsRequired", "تاریخ آغاز دوره الزامی است");

        public static Error EndDateHasToBeGreaterThanStartDate => Error.Failure("Mehvars.EndDateHasToBeGreaterThanStartDate", "تاریخ آغاز دوره نباید از تاریخ پایان بزرگتر باشد");

        public static Error SortOrderHasToBeGreaterThanZero => Error.Failure("Mehvars.SortOrderHasToBeGreaterThanZero", "ترتیب پیمایش باید بزرگتر از صفر باشد");

        public static Error OrgTypeIsRequired => Error.Failure("Mehvars.OrgTypeIsRequired", "نوع سازمان الزامی است");

    }
}
