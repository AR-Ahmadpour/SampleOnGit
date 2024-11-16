using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users
{
    internal static class UserErrors
    {
        public static Error InValidNationalCode => Error.Failure("InValidNationalCode", "نام کاربر و یا رمز عبور معتبر نمیباشد");
        public static Error GetUserByParameters => Error.Failure("GetUserByParameters", "وارد کردن یک پارامتر الزامی است");
        public static Error ErrorAddRoleUser => Error.Failure("ErrorAddRoleUser", "هنگام ثبت نقش کاربر خطا رخ داده است");
        public static Error ErrorEditRoleUser => Error.Failure("ErrorEditRoleUser", "هنگام ,ویرایش نقش کاربر خطا رخ داده است");
        public static Error DuplicateAddRoleUser => Error.Failure("DuplicateAddRoleUser", "قبلا برای کاربر نقش انتخاب شده ثبت شده است.");
        public static Error ErrorAddRoleOrganization => Error.Failure("ErrorAddRoleOrganization", "هنگام ثبت نقش کاربر - سازمان  خطا رخ داده است");
        public static Error ErroEditRoleOrganization => Error.Failure("ErroEditRoleOrganization", "هنگام ویرایش نقش کاربر - سازمان  خطا رخ داده است");
        public static Error ErroNotFoundRoleOrganization => Error.Failure("ErroNotFoundRoleOrganization", "هنگام ویرایش نقش کاربر - سازمان جهت ویرایش  یافت نشد");
        public static Error ErroNotFoundRoleUniversity => Error.Failure("ErroNotFoundRoleUniversity", "هنگام ویرایش نقش کاربر - دانشگاه جهت ویرایش  یافت نشد");
        public static Error ErroEditRoleUniversity => Error.Failure("ErroEditRoleUniversity", "هنگام ویرایش نقش کاربر - دانشگاه  خطا رخ داده است");
        public static Error DuplicateAddRoleUserInUniverSity => Error.Failure("DuplicateAddRoleUserInUniverSity", "قبلا برای کاربر نقش انتخاب شده در دانشگاه ثبت شده است.");
        public static Error DuplicateUserPermission => Error.Failure("DuplicateUserPermission", "مجوز انتخاب شده قبلا به کاربر انتساب داده شده است");
        public static Error UserNotFound => Error.Failure("UserNotFound", "کاربر یافت نشد.");
        public static Error UserNotFoundUserPermission => Error.Failure("UserNotFoundUserPermission", "مجوز کاربر یافت نشد.");

    }

}
