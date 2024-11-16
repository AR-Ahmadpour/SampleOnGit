namespace Accrediation.Application.Common.Errors.Tickets
{
    public class TicketNotFoundException: Exception
    {
        public TicketNotFoundException(string message = "تیکت مورد نظر یافت نشد")
            : base(message)
        {
            
        }
    }
}
