using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Tahsilats.GetById
{
    public sealed record GetTahsilatByUserGuidQuery(Guid UserGuid) :
IQuery<GetTahsilatByUserGuidDto>;
}
