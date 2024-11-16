namespace Accrediation.Application.Common.Errors.DepartmentUsers
{
    public class UserCanNotBeMemberOfMultipleDepartmentsException : Exception
    {
        public UserCanNotBeMemberOfMultipleDepartmentsException(string message = "در حال حاضر کاربر در یک دپارتمان عضو است و نمیتواند در دپارتمان دیگری عضو شود") 
            : base(message)
        {
            
        }
    }
}
