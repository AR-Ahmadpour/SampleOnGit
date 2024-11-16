namespace Accrediation.Application.Common.Errors.PostTemplates
{
    public class PostTemplateNotFoundException : Exception
    {
        public PostTemplateNotFoundException(string message = "قالب پست موردنظر یافت نشد") 
            : base(message)
        {
            
        }
    }
}
