namespace Accrediation.Application.Common.Errors.Departments
{
    public class DepartmentNotFoundException: Exception
    {
        public DepartmentNotFoundException(string message = "دپارتمان مورد نظر یافت نشد") 
            : base(message)
        {
            
        }
    }
}
