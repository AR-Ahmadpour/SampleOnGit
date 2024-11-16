using SharedKernel;

namespace Accreditation.Application.FileTables
{
    public static class FileTableErrors
    {
        public static Error AddError => Error.Conflict("FileTables.AddError", "هنگام ذخیره فایل خطا رخ داده است");
        public static Error DeleteError => Error.Conflict("FileTables.DeleteError", "هنگام حذف فایل خطا رخ داده است");


    }
}
