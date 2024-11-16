using System;

namespace Accrediation.Application.Common.Errors
{
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException(string message = "فایل مورد نظر یافت نشد")
            : base(message)
        {

        }
    }
}
