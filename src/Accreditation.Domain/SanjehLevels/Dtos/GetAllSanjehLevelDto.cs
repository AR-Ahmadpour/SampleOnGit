using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Domain.SanjehLevels.Dtos
{
    public sealed class GetAllSanjehLevelDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Guid Guid { get; set; }
    }
   
}
