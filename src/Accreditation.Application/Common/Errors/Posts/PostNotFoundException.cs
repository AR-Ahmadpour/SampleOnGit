namespace Accrediation.Application.Common.Errors.Posts
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(string message = "پست مورد نظر یافت نشد") 
            : base(message)
        {
            
        }
    }
}
