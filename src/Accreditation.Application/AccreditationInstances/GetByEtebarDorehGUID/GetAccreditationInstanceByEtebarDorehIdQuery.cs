using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.AccreditationInstances.GetByEtebarDorehGUID
{
    public sealed record GetAccreditationInstanceByEtebarDorehIdQuery
        (Guid EtebarDorehId):IQuery<List<GetAccreditationInstanceByEtebarDorehIdQueryDto> >;
   
}
