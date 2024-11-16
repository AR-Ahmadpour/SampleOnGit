using SharedKernel;

namespace Accreditation.Application.CountryDivisions.BakhshLocation
{
    public static class BakhshErrors
    {
        public static Error NotFoundBakhsh => Error.NotFound("Bakhsh.NotFound", " شناسه بخش یافت نشد");
    }
}
