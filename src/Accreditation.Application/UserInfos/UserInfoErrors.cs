using SharedKernel;

namespace Accreditation.Application.UserInfos
{
    public static class UserInfoErrors
    {
        public static Error NotFoundUserInfo => Error.NotFound("UserInfo.NotFoundUserInfo", " رکورد مورد نظر برای ویرایش یافت نشد");

    }
}
