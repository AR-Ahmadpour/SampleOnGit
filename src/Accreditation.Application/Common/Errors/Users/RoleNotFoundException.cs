using System;

namespace Accrediation.Application.Common.Errors.Users
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string message = "نقش موردنظر یافت نشد")
            : base(message)
        {

        }
    }
}
