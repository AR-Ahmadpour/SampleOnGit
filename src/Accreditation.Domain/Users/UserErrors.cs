using SharedKernel;

namespace Accreditation.Domain.Users;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound("User.Found", "The user with the specified identifier was not found");

    public static Error InvalidCredentials => Error.NotFound("User.InvalidCredentials", "نام کاربری و یا رمز عبور معتبر نمی باشد");//"The provided credentials were invalid");
    public static Error ExpireToken => Error.Problem("User.InvalidCredentials", "توکن معتبر نمی باشد مجددا وارد سامانه شوید");//"The provided credentials were invalid");

    public static Error ListRoleUserUniversity => Error.Problem("ListRoleUserUniversity", "اطلاعات دسترسی برای کاربر یافت نشد");

}
