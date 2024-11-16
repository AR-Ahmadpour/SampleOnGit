namespace Accrediation.Application.Common.Errors.Categories
{
    public class CategoryNotFoundInThisHelpDeskException: Exception
    {
        public CategoryNotFoundInThisHelpDeskException(string message = "دسته بندی مورد نظر در این میز خدمت وجود ندارد") 
            : base(message)
        {
            
        }
    }
}
