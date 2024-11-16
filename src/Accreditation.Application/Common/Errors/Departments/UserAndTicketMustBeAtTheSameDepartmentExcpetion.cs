namespace Accrediation.Application.Common.Errors.Departments
{
    public class UserAndTicketMustBeAtTheSameDepartmentExcpetion : Exception 
    {
        public UserAndTicketMustBeAtTheSameDepartmentExcpetion(string message = "شما در دپارتمانی که تیکت ثبت شده قرار ندارید") 
            : base(message)
        {
            
        }
    }
}
