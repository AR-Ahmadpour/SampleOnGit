namespace Accrediation.Application.Common.Errors.Tickets
{
    public class OnlyThicketSenderCanCancelTheTicketException : Exception
    {
        public OnlyThicketSenderCanCancelTheTicketException(string message = "فقط شخص ایجاد کننده‌ تیکت قادر به لغو کردن تیکت می‌باشد") 
            : base(message)
        {
            
        }
    }
}
