using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli
{
    public sealed record GetListStandardsInArzyabiDakheliQuery
    (
    Guid ZirMehvarGUID,
    bool SanjehNA,
    bool SanjehMe,
    int SanjehLevelId,
    Guid AccInstanceID,
    Guid EtebardorehId
    ) : IQuery<List<GetListStandardsInArzyabiDakheliDto>>;
}
