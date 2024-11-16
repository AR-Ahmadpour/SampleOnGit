using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Standards.GetById
{
    public sealed record GetStandardResponse
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Code { get; set; }
        public int SortOrder { get; set; }
        public int? WeightedCoefficient { get; set; }
        public bool IsDeleted { get; set; }    
    }
}
