namespace Accrediation.Application.Common.Errors.Users
{
    public class YourAccountHasBeenBlockedException: Exception
    {
        public YourAccountHasBeenBlockedException(string message = "حساب کاربری شما مسدود شده است")
            : base(message)
        {

        }
    }
}
