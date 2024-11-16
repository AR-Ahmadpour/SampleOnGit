using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.ReshtehMaghtas.GetList
{
    public sealed record GetListReshtehMaghtaTahsiliQuery(Guid MaghtaGuid) :
IQuery<List<GetListReshtehMaghtaQueryDto>>;
}
