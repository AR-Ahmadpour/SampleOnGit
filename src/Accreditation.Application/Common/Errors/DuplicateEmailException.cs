using System;

namespace Accrediation.Application.Common.Errors
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(
            string message = "ایمیل وارد شده تکراری است") : base(message)
        {

        }
    }
}
