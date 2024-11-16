namespace Accrediation.Application.Common.Errors.HelpDesks
{
    public class HelpDeskTitleAlreadyExistsException : Exception
    {
        public HelpDeskTitleAlreadyExistsException(string message = "ثبت میزخدمت با عنوان تکراری ممکن نیست") 
            : base (message)
        {
            
        }
    }
}
