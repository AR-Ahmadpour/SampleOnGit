using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli
{
    public sealed class GetListZirMehvarsInArzyabiDakheliDto
    {
        public Guid? MehvarId { get; set; }
        public Guid ZirMehvarId { get; set; }
        public Guid? AccInstanceID { get; set; }
        public string? ZirMehvarTitel { get; set; }
        public int SanjehCount { get; set; }
        public int ResultCount { get; set; }

        /// <summary>
        /// صفر: همه پاسخ داده شده است
        /// یک: تعداد پاسخ داده شده است
        /// دو :هیچکدام پاسخ داده نشده است
        /// </summary>
        public int FlagState { get; set; }
    }
}
