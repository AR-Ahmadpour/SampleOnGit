using System;

namespace Accrediation.Application.Common.Errors
{
    public class PasswordAndConfirmPasswordMismatchException : Exception
    {
        public PasswordAndConfirmPasswordMismatchException(string message = "عدم مطابقت کلمه عبور و تایید کلمه عبور")
            : base(message)
        {

        }
    }
}
