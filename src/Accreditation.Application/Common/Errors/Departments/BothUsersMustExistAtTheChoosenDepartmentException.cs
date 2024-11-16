namespace Accrediation.Application.Common.Errors.Departments
{
    public class BothUsersMustExistAtTheChoosenDepartmentException: Exception
    {
        public BothUsersMustExistAtTheChoosenDepartmentException(string message = "کاربر ارجاع دهنده و کاربر دریافت کننده ی ارجاع باید در دپارتمان مورد نظر عضو باشند") 
            : base(message)
        {
            
        }
    }
}
