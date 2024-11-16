namespace Accrediation.Application.Common.Errors.Categories
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string message = "دسته بندی مورد نظر یافت نشد")
            : base(message)
        {
            
        }
    }
}
