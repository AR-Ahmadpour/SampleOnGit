namespace Accrediation.Application.Common.Errors.DepartmentUserCategories
{
    public class DepartmentUserCategoryNotFoundException : Exception
    {
        public DepartmentUserCategoryNotFoundException(string message = "کاربری با مشخصات وارد شده در دسته بندی مورد نظر یافت نشد") : base(message)
        {
            
        }
    }
}
