namespace Accrediation.Application.Common.Errors.Tickets
{
    public class TicketCanNotReferMoreThanTwoTimesToParentDepartmentException: Exception
    {
        public TicketCanNotReferMoreThanTwoTimesToParentDepartmentException(string message = "شما نمیتوانید بیشتر از دو بار تیکت را به دپارتمان بالادستی ارجاع دهید") 
            : base(message)
        {
            
        }
    }
}
