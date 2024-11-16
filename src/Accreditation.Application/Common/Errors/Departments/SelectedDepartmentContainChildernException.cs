namespace Accrediation.Application.Common.Errors.Departments
{
    public class SelectedDepartmentContainChildernException : Exception
    {
        public SelectedDepartmentContainChildernException(string message = "دپارتمان مورد نظر شامل دپارتمان زیر دست می باشد") 
            : base(message)
        {
            
        }
    }
}
