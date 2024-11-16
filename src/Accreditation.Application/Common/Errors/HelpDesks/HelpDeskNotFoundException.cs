namespace Accrediation.Application.Common.Errors.HelpDesks
{
    public class HelpDeskNotFoundException: Exception
    {
        public HelpDeskNotFoundException(string message = "میزخدمت مورد نظر یافت نشد")
            : base(message)
        {
            
        }
    }
}
