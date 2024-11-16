using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli
{ 
    public sealed record GetListZirMehvarsInArzyabiDakheliQuery
    ( Guid MehvarsId,
    bool SanjehNA,
    bool SanjehMe,
    int SanjehLevelId,
    Guid AccInstanceID,
    Guid EtebardorehId
    ) : IQuery<List<GetListZirMehvarsInArzyabiDakheliDto>>;
}
