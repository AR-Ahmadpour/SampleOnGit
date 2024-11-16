using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.AccreditationalInstanceAnswers
{
    internal static  class AccreditationalInstanceAnswerError
    {
        public static Error ErroNotFoundAccreditationalInstanceAnswer => Error.Failure("ErroNotFoundAccreditationalInstanceAnswer", "اطلاعات پاسخ جهت ویرایش  یافت نشد");
        public static Error ErroWhenAccreditationalInstanceAnswer => Error.Failure("ErroWhenAccreditationalInstanceAnswer", "هنگام ثبت اطلاعات پاسخ خطا رخ داده است");
        public static Error Erro_NA_AccreditationalInstanceAnswer => Error.Failure("Erro_NA_AccreditationalInstanceAnswer", "هنگامی که پاسخ NA باشد وارد کردن توضیحات الزامی مبباشد");
        public static Error ErroNoResponseAccreditationalInstanceAnswer => Error.Failure("ErroNoResponseAccreditationalInstanceAnswer", "مقدار بدون پاسخ معتبر نمبباشد");

    }
}
