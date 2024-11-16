namespace Accrediation.Application.Common.Errors.Departments
{
    public class YouAreNotMemberOfAnyExistingDepartmentsException : Exception
    {
        public YouAreNotMemberOfAnyExistingDepartmentsException(string message = "شما عضو هیچ دپارتمانی نیستید") 
            : base(message)
        {
            
        }
    }
}
