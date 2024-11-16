namespace Accrediation.Application.Common.Errors.Departments
{
    public class DepartmentDoesNotExistInThisHelpDeskException : Exception
    {
        public DepartmentDoesNotExistInThisHelpDeskException(string message = "دپارتمان مورد نظر در این میز خدمت یافت نشد") 
            : base(message)
        {
            
        }
    }
}
