using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli
{
    public sealed record GetListMehvarsInArzyabiDakheliQuery
    (
    Guid EtebardorehId,
    bool SanjehNA,
    bool SanjehMe,
    int SanjehLevelId,
    Guid AccInstanceID
    ) :IQuery<List<GetListMehvarsInArzyabiDakheliQueryDto>>;
}
