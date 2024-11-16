namespace Accrediation.Application.Common.Errors.DepartmentUsers
{
    public class DepartmentUserNotFoundException : Exception
    {
        public DepartmentUserNotFoundException(string message = "این کاربر در هیچ دپارتمانی وجود ندارد") 
            : base(message)
        {
            
        }
    }
}
