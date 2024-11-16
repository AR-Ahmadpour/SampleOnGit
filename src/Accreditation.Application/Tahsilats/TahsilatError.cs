using SharedKernel;

namespace Accreditation.Application.Tahsilats
{
    public static class TahsilatError
    {
        public static Error TahsilatNotFound => Error.NotFound("Tahsilat.NotFound", "رکورد مورد نظر برای ویرایش یافت نشد");
    }
}
