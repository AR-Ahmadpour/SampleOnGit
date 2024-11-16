using SharedKernel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.SanjehFields
{
    public static class SanjehFieldError
    {
        public static Error Exist => Error.Conflict("SanjehField.Exist", "سنجه در این بسته قبلا ثبت شده است");

    }
}
