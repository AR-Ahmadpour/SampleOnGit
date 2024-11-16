namespace Accrediation.Application.Common.Errors.Departments
{
    public class ThereIsNoParentDepartmentForThisDepartmentException : Exception
    {
        public ThereIsNoParentDepartmentForThisDepartmentException(string message = "دپارتمان انتخاب شده، دپارتمان بالادستی ندارد") 
            : base(message)
        {
            
        }
    }
}
