namespace Accrediation.Application.Common.Errors.Users
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message = "کاربر مورد نظر یافت نشد")
            : base(message)
        {
            
        }
    }
}
