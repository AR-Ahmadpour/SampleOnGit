using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace Accreditation.Application.Common.Enums
{
    public struct PermisionEnum
    {
        public const string CreateEvaluation = "CreateEvaluation";// "ایجاد برنامه ارزیابی";
        public const string UpdateEvaluation = "UpdateEvaluation";// "ویرایش برنامه ارزیابی";
        public const string ArchiveEvaluation = "ArchiveEvaluation";//  "بایگانی کردن برنامه ارزیابی";
        public const string CreateEtebarDoder = "CreateEtebarDoder";// "ایجاد دوره اعتباربخشی";
        public const string UpdateEtebarDoder = "UpdateEtebarDoder";// "ویرایش دوره اعتباربخشی";
        public const string DeleteEtebarDoder = "DeleteEtebarDoder";// "حذف دوره اعتباربخشی";
        public const string CreateMehvar = "CreateMehvar";//  "ایجاد محور";
        public const string CreateZirMehvar = "CreateZirMehvar";// "ایجاد زیر محور";
        public const string GetEtebarDore = "CreateZirMehvar";// "مشاهده دوره اعتبار بخشی";
        public const string DeleteLogicalEtebarDoder = "CreateZirMehvar";// "بایگانی دوره اعتباربخشی";
        public const string AddUserRole = "CreateZirMehvar";// "ایجاد نقش برای کاربر";
        public const string ToggleStateUserRole = "CreateZirMehvar";// "تغییر وضعیت نقش کاربر";
        public const string ToggleStateUser = "ToggleStateUser";// "تغییر وضعیت فعالیت کاربر";
        public const string GetAllUsers = "GetAllUsers"; // نمایش لیست کاربران 
    }
}
