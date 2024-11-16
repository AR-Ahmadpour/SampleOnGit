using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli
{

    public sealed class GetListStandardsInArzyabiDakheliDto
    {
        public Guid StandardId { get; set; }
        public Guid? ParentZirMehvarId { get; set; }
        public Guid AccInstanceID { get; set; }
        public string? StandardTitel { get; set; }
        public int SanjehCount { get; set; }
        public int ResultCount { get; set; }

        /// <summary>
        /// صفر: همه پاسخ داده شده است
        /// یک: تعداد پاسخ داده شده است
        /// دو :هیچکدام پاسخ داده نشده است
        /// </summary>
        public int FlagState { get; set; }
        public Guid EtebardorehId { get; set; }
    }
}
