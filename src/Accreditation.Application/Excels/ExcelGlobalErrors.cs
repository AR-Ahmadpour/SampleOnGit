using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Excels
{
    public static class ExcelGlobalErrors
    {
        public static Error ArzyabError => Error.Conflict("Excel.UserGuid", "اطلاعات ارزیاب اشتباه است");
        public static Error AccINstanceError => Error.Conflict("Excel.AccInstanceError", "اطلاعات ماموریت اعتبار بخشی اشتباه است");
        public static Error FieldError => Error.Conflict("Excel.UserGuid", "اطلاعات بسته ارزیابی اشتباه است");
    }
}
