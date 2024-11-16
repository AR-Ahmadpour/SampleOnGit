using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.NotNaSanjehs.GetList
{
    public sealed record GetListOrgGerayeshBySanjehIdQuery(Guid SanjehGuid) :
IQuery<List<GetListOrgGerayeshBySanjehIdDto>>;
}
