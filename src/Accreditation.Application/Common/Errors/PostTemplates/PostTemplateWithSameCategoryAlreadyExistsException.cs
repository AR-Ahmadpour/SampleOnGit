namespace Accrediation.Application.Common.Errors.PostTemplates
{
    public class PostTemplateWithSameCategoryAlreadyExistsException : Exception
    {
        public PostTemplateWithSameCategoryAlreadyExistsException(string message = "قالب پست با دسته بندی مشابه قبلا ثبت شده") 
            : base(message)
        {
            
        }
    }
}
