using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.OrgGerayesh
{
    public static class OrgGerayeshErrors
    {
        public static Error NotFound => Error.NotFound("Sanjeh.NotFound", "  گرایش سازمان مورد نظر یافت نشد");
        
    }
}
