namespace Accrediation.Application.Common.Errors.Categories
{
    public class CategoryAlreadyExistsInThisHelpDeskException: Exception
    {
        public CategoryAlreadyExistsInThisHelpDeskException(
            string message = "عنوان دسته بندی وارد شده تکراری می‌ باشد") : base(message)
        {
            
        }
    }
}
