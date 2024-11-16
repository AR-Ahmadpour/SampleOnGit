namespace Accrediation.Application.Common.Errors.Users
{
    public class WrongUsernameOrPasswordException: Exception
    {
        public WrongUsernameOrPasswordException(string message = "نام کاربری یا کلمه عبور اشتباه است") 
            : base(message)
        {

        }
    }
}
