namespace Accrediation.Application.Common.Errors.DepartmentUserCategories
{
    public class DepartmentUserAlreadyExistsInThisCategoryException : Exception
    {
        public DepartmentUserAlreadyExistsInThisCategoryException(string message = "این کاربر در این دسته بندی عضو می‌ باشد") 
            : base(message)
        {
            
        }
    }
}
