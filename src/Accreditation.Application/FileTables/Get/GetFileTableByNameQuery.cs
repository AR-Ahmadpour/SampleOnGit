using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Get
{
    public sealed record  GetFileTableByNameQuery(string FileName):IQuery<GetFileTableByNameDto>;
  
}
