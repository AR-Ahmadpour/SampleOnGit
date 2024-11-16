namespace Accrediation.Application.Common.Errors.Posts
{
    public class YouCanNotAddTwoContinuousPostsOnATicketException: Exception
    {
        public YouCanNotAddTwoContinuousPostsOnATicketException(string message = "شما نمی توانید دو پست پشت سر هم در یک تیکت ثبت کنید") : base(message)
        {
            
        }
    }
}
