using SharedKernel;

namespace Accreditation.Application.UserDorehs
{
    public static class UserDorehErrors
    {
        public static Error DorehAmoozeshiNotFound => Error.NotFound("DorehAmoozeshi.DorehAmoozeshiNotFound", "دوره آموزشی یافت نشد");
        public static Error UserDorehFound => Error.NotFound("UserDoreh.UserDorehNotFound", "دوره فرد یافت نشد");
        public static Error UserFound => Error.NotFound("UserDoreh.UserFound", "شتاسه فرد یافت نشد");
        
    }
}
