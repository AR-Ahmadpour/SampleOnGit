namespace Accrediation.Application.Common.Errors.Users
{
    public class YourEmailIsNotConfirmedYetException: Exception
    {
        public YourEmailIsNotConfirmedYetException(string message = "Your Email Is Not Confirmed Yet!") : base(message)
        {

        }
    }
}
