using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Get
{
    public sealed record GetFileTableByNameDto
    {
        public Guid stream_id { get; set; }
        public byte[]? file { get; set; }
        public string? Name { get; set; }
        public string? filetype { get; set; }
        public long Size { get; set; }
    }
}
