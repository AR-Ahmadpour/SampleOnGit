using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Universityes.GetAll
{
    public sealed record GetAllUniversityDto
    {
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public string Title { get; set; }

    }
}
