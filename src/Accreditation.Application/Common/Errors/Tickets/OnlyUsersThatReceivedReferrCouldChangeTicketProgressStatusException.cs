namespace Accrediation.Application.Common.Errors.Tickets
{
    public class OnlyUsersThatReceivedReferrCouldChangeTicketProgressStatusException : Exception
    {
        public OnlyUsersThatReceivedReferrCouldChangeTicketProgressStatusException(string message = "تنها کاربرانی که این تیکت به آنها ارجاع داده شده میتوانند وضعیت تیکت را تغییر دهند") 
            : base(message)
        {
            
        }
    }
}
