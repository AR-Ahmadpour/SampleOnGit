using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.NotNaSanjehs
{
    public static class NotNaSanjehErrors
    {
        public static Error AllOrgGerayeshAlreadyAssigned => Error.NotFound("orgGerayesh.NotFound", "تمام گرایش ها برای این سنجه قبلا ثبت شده اند");
    }
}
