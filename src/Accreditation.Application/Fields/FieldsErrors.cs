using SharedKernel;

namespace Accreditation.Application.Fields;
public static class FieldsErrors
{
    public static Error NotFound => Error.NotFound("Field.NotFound", "فیلد بسته ارزیابی یافت نشد");
    public static Error NotActive => Error.NotFound("Field.NotFound", "فیلد بسته ارزیابی غیر فعال شده");
}