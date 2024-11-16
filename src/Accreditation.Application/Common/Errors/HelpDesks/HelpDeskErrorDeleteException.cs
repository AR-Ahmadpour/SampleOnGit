using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accrediation.Application.Common.Errors.HelpDesks
{
    public class HelpDeskErrorDeleteException:Exception
    {
        public HelpDeskErrorDeleteException(string message = "میزخدمت فعال نظر یافت نشد")
             : base(message) { }
    }
}
