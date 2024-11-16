namespace Accrediation.Application.Common.Errors.Tickets
{
    public class YouHaveNotReferredAnyTicketsToThisUserRecentlyException : Exception
    {
        public YouHaveNotReferredAnyTicketsToThisUserRecentlyException(string message = "شما تاکنون تیکت انتخاب شده را به این کاربر ارجاع نداده اید") 
            : base(message)
        {

        }
    }
}
