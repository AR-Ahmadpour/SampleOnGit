using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.AccreditationInstances.GetByEtebarDorehGUID
{
    public sealed record GetAccreditationInstanceByEtebarDorehIdQueryDto
    {
        public Guid GUID { get; set; }
        public string? ArzyabiType { get ; set; }
        public string? StartDate { get; set; }
        public string? UniversityName { get; set; }
        public string? OrganizationName { get; set; }
        public string? ActionLink { get; set; }
        public bool? SendStatus { get; set; }
        public string? state { get; set; }


    }
}
