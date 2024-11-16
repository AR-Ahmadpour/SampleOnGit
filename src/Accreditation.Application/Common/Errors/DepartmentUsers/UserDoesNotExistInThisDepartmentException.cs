namespace Accrediation.Application.Common.Errors.DepartmentUsers
{
    public class UserDoesNotExistInThisDepartmentException : Exception
    {
        public UserDoesNotExistInThisDepartmentException(string message = "کاربر انتخاب شده در این دپارتمان وجود ندارد") 
            : base(message)
        {
            
        }
    }
}
